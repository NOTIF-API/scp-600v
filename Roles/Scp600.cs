using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Server;
using MEC;

using PlayerRoles;

using SCP_600V.API.Extensions;

using YamlDotNet.Serialization;

namespace SCP_600V.Roles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class Scp600v : CustomRole
    {
        [YamlIgnore]
        public const string IsScp600 = "IsScp600";
        [YamlIgnore]
        private const string apperancekey = "Apperance";

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
        [Description("Will the player and Scp be able to attack each other (SCP VS Scp600)")]
        public bool IsFf { get; set; } = false;
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
            player.SessionVariables.Add(apperancekey, StartApperance);
            player.SessionVariables.Add(IsScp600, null);
            Timing.RunCoroutine(ApperanceUpdate(player), $"{player.Id}-apperance-updater");
            Timing.CallDelayed(0.5f, () =>
            {
                if (string.IsNullOrWhiteSpace(CustomInfo)) // if there are empty lines or a hint that there will be empty lines, we set the basic information about the player
                {
                    player.CustomInfo = string.Empty;
                    player.InfoArea |= PlayerInfoArea.Nickname | PlayerInfoArea.Role;
                }
                ChangeApperance(player, StartApperance);
            });
        }
        protected override void RoleRemoved(Player player) 
        {
            Timing.KillCoroutines($"{player.Id}-apperance-updater");
            player.SessionVariables.Remove(IsScp600);
            player.SessionVariables.Remove(apperancekey);
            base.RoleRemoved(player); 
        }
        // updates scp 600 skin for all players every 15 seconds
        private IEnumerator<float> ApperanceUpdate(Player player)
        {
            for (; ; )
            {
                if (player == null) { break; }
                if (player.IsDead) { break; }
                if (player.SessionVariables.ContainsKey(apperancekey))
                {
                    player.ChangeAppearance((RoleTypeId)player.SessionVariables[apperancekey]);
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
            if (Server.PlayerCount > MinPlayerCount && UnityEngine.Random.value < ( SpawnChance / 100))
            {
                //spawn random selectable role!
                try
                {
                    Player[] s = Player.List.Where(x => Main.Instance.Config.SelectableRoles.Contains(x.Role.Type)).ToArray();
                    s[UnityEngine.Random.Range(0, s.Length - 1)].SpawnAs600();
                }
                catch (Exception ex)
                {
                    Log.Debug($"{nameof(OnRoundStarted)}: {ex.Message}");
                }
            }
        }

        private void OnEndingRound(EndingRoundEventArgs e)
        {
            if (e.LeadingTeam == LeadingTeam.Anomalies) return;
            if (e.IsForceEnded) return;
            if (TrackedPlayers.Count == 0) return;
            if (Player.List.Where(x => !x.IsDead && !x.IsScp600() && !x.IsScp).Count() == 0) return;
            e.IsAllowed = false;
        }

        private void OnEntaringHazards(EnteringEnvironmentalHazardEventArgs e)
        {
            if (IsScpInteractWithPlayer) return;
            if (e.Player == null | !Check(e.Player)) return;
            e.IsAllowed = false;
        }
        private void OnHurting(HurtingEventArgs e)
        {
            if (IsFf) return; // if it is possible to fight then let it be
            if (e.Attacker == null | e.Player == null) return; // if someone does not exist then there is no point in processing
            if (!Check(e.Attacker) | !Check(e.Player)) return; // if there is no object among us, then the point of processing disappears
            if (e.Attacker.IsScp | e.Player.IsScp | e.Attacker.IsScp600() | e.Player.IsScp600()) // if someone is Scp or object then we cancel the event
            {
                e.Amount = 0;
                e.IsAllowed = false;
            }
        }
        private void OnEnteringPocketDemension(EnteringPocketDimensionEventArgs e)
        {
            if (IsScpInteractWithPlayer) return;
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
            e.Attacker.SessionVariables[apperancekey] = e.TargetOldRole;
            Log.Debug($"{nameof(OnDied)}: {e.Player.DisplayNickname}-{e.Player.IsScp600()} killed by {e.Attacker.DisplayNickname}-{e.Attacker.IsScp600()}");
        }
        private void OnPickupingItem(PickingUpItemEventArgs e)
        {
            if (e.Player == null | !Check(e.Player)) return;
            if (!BlackListItems.Contains(e.Pickup.Type)) return;
            e.IsAllowed = false;
        }

        public static void ChangeApperance(Player ply, RoleTypeId role)
        {
            if (ply == null) return;
            if (!ply.IsScp600()) return;
            ply.ChangeAppearance(role);
            ply.SessionVariables[apperancekey] = role;
        }
    }
}