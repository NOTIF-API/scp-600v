using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using AdminToys;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Core.UserSettings;
using Exiled.API.Features.Spawn;
using Exiled.API.Features.Toys;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MEC;

using Mirror;

using PlayerRoles;
using PlayerRoles.Voice;

using SCP_600V.API.Extensions;

using UnityEngine;

using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Networking;

using YamlDotNet.Serialization;

namespace SCP_600V.Roles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class Scp600v : CustomRole
    {
        public override uint Id { get; set; } = 600;

        public override int MaxHealth { get; set; } = 400;
        [YamlIgnore]
        public override string Name { get; set; } = "SCP-600V";
        public override string Description { get; set; } = "An object performing a task as an aggressive object against humanity";
        [Description("Information visible on the role, to hide it make the line empty")]
        public override string CustomInfo { get; set; } = "SCP-600";
        [Description("The message that a player sees after killing another player")]
        public string KillMessage { get; set; } = "You killed player %player% and changed your appearance to %role%";

        [Description("Basically will 173, 106, 939 be able to apply abilities to our object")]
        public bool IsScpInteractWithPlayer { get; set; } = false;
        [Description("Will the player get AHP when killing a player")]
        public bool IsAhpRenerate { get; set; } = true;
        [Description("When killed, will the player also increase the maximum amount of AHP divided by two")]
        public bool IsAhpMaxIncrease { get; set; } = true;
        [Description("The amount of AHP that a player will receive when killing another player")]
        public int AhpAmount { get; set; } = 15;
        [Description("Object Spawn Probability")]
        public override float SpawnChance { get; set; } = 25;
        [Description("Minimum player online for spawning role")]
        public int MinPlayerCount { get; set; } = 8;
        [Description("List of items that the object cannot take, ItemType as the base representation of the item names")]
        public List<ItemType> BlackListItems { get; set; } = new List<ItemType>()
        {
            ItemType.MicroHID,
            ItemType.Jailbird
        };
        [Description("List of items that the player will have when receiving the role (do not give what is prohibited)")]
        public override List<string> Inventory { get; set; } = new List<string>()
        {
            ItemType.Coin.ToString(),
            ItemType.Adrenaline.ToString()
        };
        [Description("Initial appearance of the object upon respawn")]
        public RoleTypeId StartApperance { get; set; } = RoleTypeId.ClassD;
        [YamlIgnore]
        public override SpawnProperties SpawnProperties { get; set; }
        [YamlIgnore]
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        [YamlIgnore]
        public override List<CustomAbility> CustomAbilities { get; set; }
        [YamlIgnore]
        public override Dictionary<AmmoType, ushort> Ammo { get; set; }
        [YamlIgnore]
        public override bool KeepPositionOnSpawn { get; set; } = true;
        [YamlIgnore]
        public override bool KeepRoleOnDeath { get; set; } = false;
        [YamlIgnore]
        public override bool KeepRoleOnChangingRole { get; set; } = false;
        [YamlIgnore]
        public override bool RemovalKillsPlayer { get; set; } = true;
        [YamlIgnore]
        public override bool IgnoreSpawnSystem { get; set; } = true;

        protected override void RoleAdded(Player player)
        {
            base.RoleAdded(player);
            player.SessionVariables.Add("apperance", StartApperance);
            player.SessionVariables.Add("IsScp600", null);
            Timing.RunCoroutine(ApperanceUpdate(player), $"{player.Id}-apudp");
            Timing.CallDelayed(0.5f, () =>
            {
                if (CustomInfo == "" | CustomInfo == string.Empty)
                {
                    player.CustomInfo = string.Empty;
                    player.InfoArea |= PlayerInfoArea.Nickname | PlayerInfoArea.Role;
                }
                player.ChangeAppearance(StartApperance);
            });
        }
        protected override void RoleRemoved(Player player) 
        {
            Timing.KillCoroutines($"{player.Id}-apudp");
            player.SessionVariables.Remove("IsScp600");
            player.SessionVariables.Remove("apperance");
            base.RoleRemoved(player); 
        }
        // updates scp 600 skin for all players every 15 seconds
        private IEnumerator<float> ApperanceUpdate(Player player)
        {
            for (; ; )
            {
                if (player == null) { break; }
                if (player.IsDead) { break; }
                if (player.SessionVariables.ContainsKey($"apperance"))
                {
                    player.ChangeAppearance((RoleTypeId)player.SessionVariables["apperance"]);
                    Log.Debug($"{nameof(ApperanceUpdate)}: Updated apperance for {player.DisplayNickname}");
                }
                yield return Timing.WaitForSeconds(15);
            }
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard += OnEntaringHazards;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += OnEnteringPocketDemension;
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickupingItem;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.EndingRound += OnEndingRound;
        }
        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard -= OnEntaringHazards;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= OnEnteringPocketDemension;
            Exiled.Events.Handlers.Player.Died -= OnDied;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickupingItem;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.EndingRound -= OnEndingRound;
        }
        // i don't trust CustomRole spawn system lol
        private void OnRoundStarted()
        {
            if (Server.PlayerCount > MinPlayerCount && UnityEngine.Random.value < (SpawnChance/100))
            {
                //spawn random class D!
                try
                {
                    Player.List.Where(x => x.Role.Type == RoleTypeId.ClassD).FirstOrDefault().SpawnAs600();
                }
                catch (Exception ex)
                {
                    Log.Debug(ex.Message);
                }
            }
        }

        private void OnEndingRound(EndingRoundEventArgs e)
        {
            if (e.LeadingTeam == LeadingTeam.Anomalies) return;
            else
            {
                int customs = TrackedPlayers.Count;
                if (customs == 0) return;
                int targets = Player.List.Where(x => x.IsAlive && !x.IsScp && !x.IsScp600() && x.Role.Type != RoleTypeId.Tutorial).Count();
                if (targets == 0) return;
                e.IsAllowed = false;
            }
        }

        private void OnEntaringHazards(EnteringEnvironmentalHazardEventArgs e)
        {
            if (e.Player == null | !Check(e.Player) | IsScpInteractWithPlayer) return;
            e.IsAllowed = false;
        }
        private void OnHurting(HurtingEventArgs e)
        {
            if (e.Attacker == null | e.Player == null) return;
            if (Check(e.Attacker) && e.Player.Role.Side == Side.Scp)
            {
                e.Amount = 0;
                e.IsAllowed = false;
                return;
            }
            if (Check(e.Player) && e.Attacker.Role.Side == Side.Scp)
            {
                e.Amount = 0;
                e.IsAllowed = false;
                return;
            }
        }
        private void OnEnteringPocketDemension(EnteringPocketDimensionEventArgs e)
        {
            if (e.Player == null | !Check(e.Player)) return;
            e.IsAllowed = false;
        }
        private void OnDied(DiedEventArgs e)
        {
            if (e.Player == null | e.Attacker == null | !Check(e.Attacker)) return;
            if (IsAhpRenerate)
            {
                e.Attacker.ArtificialHealth += AhpAmount;
            }
            if (IsAhpMaxIncrease)
            {
                e.Attacker.MaxArtificialHealth += (AhpAmount / 2);
            }
            e.Attacker.ShowHint(KillMessage.Replace("%player%", e.Player.DisplayNickname).Replace("%role%", e.TargetOldRole.ToString()), 4);
            e.Attacker.ChangeAppearance(e.TargetOldRole);
            e.Attacker.SessionVariables[$"apperance"] = e.TargetOldRole;
            Log.Debug($"{nameof(OnDied)}: {e.Player.DisplayNickname}-{e.Player.IsScp600()} killed by {e.Attacker.DisplayNickname}-{e.Attacker.IsScp600()}");
        }
        private void OnPickupingItem(PickingUpItemEventArgs e)
        {
            if (e.Player == null | !Check(e.Player)) return;
            if (!BlackListItems.Contains(e.Pickup.Type)) return;
            e.IsAllowed = false;
        }
    }
}