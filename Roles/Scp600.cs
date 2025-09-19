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
using Exiled.Events.EventArgs.Server;
using MEC;

using PlayerRoles;

using SCP_600V.API.Extensions;
using SCP_600V.Roles.Abilities;

using YamlDotNet.Serialization;

namespace SCP_600V.Roles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class Scp600v : CustomRole
    {
        [YamlIgnore]
        private const string apperancekey = "Apperance";
        [YamlIgnore]
        public static Scp600v RegisteredInstance { get; internal set; }

        [Description("Role special id (Don't allow similarities with other roles!)")]
        public override uint Id { get; set; } = 600;

        [Description("Scp-600 max health")]
        public override int MaxHealth { get; set; } = 400;

        [YamlIgnore]
        public override string Name { get; set; } = "SCP-600V";

        [Description("Description when a role appears, as a rule, it is displayed on the player’s screen when he appears for a certain role.")]
        public override string Description { get; set; } = "An object performing a task as an aggressive object against humanity";

        [Description("Information visible on the role, to hide it make the line empty")]
        public override string CustomInfo { get; set; } = "SCP-600";

        [Description("The message that a player sees after killing another player")]
        public string KillMessage { get; set; } = "You killed player %player% and changed your appearance to %role%";

        [Description("Will the player and Scp be able to attack each other (SCP VS Scp600) and SCP Ability work on 600")]
        public bool IsFrindleFireEnabled { get; set; } = false;

        [Description("Will the player get AHP when killing a player")]
        public bool IsAhpRenerate { get; set; } = true;

        [Description("When killed, will the player also increase the maximum amount of AHP divided by two")]
        public bool IsAhpMaxIncrease { get; set; } = true;

        [Description("The amount of AHP that a player will receive when killing another player")]
        public int AhpAmount { get; set; } = 15;

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

        [Description("Basic role spawn settings (location chose)")]
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit=1,
            RoleSpawnPoints=new()
            {
                new()
                {
                    Chance=100,
                    Role=RoleTypeId.ClassD
                }
            }
        };
        [Description("Chance for a player to appear for a given role at the start of a round")]
        public override float SpawnChance { get; set; } = 25f;
        [Description("The role that the player has on his own behalf (it is not advisable to change it, as it is a human role)")]
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;

        [Description("Role-specific abilities that can be granted")]
        public override List<CustomAbility> CustomAbilities { get; set; } = new()
        {
            new ApperanceUpdate()
        };

        [Description("The initial amount and type of ammo a player can have when they spawn for a given role.")]
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
        {
            { AmmoType.Nato9, 20 }
        };
        [YamlIgnore]
        public override bool KeepRoleOnDeath { get; set; } = false;
        [YamlIgnore]
        public override bool KeepRoleOnChangingRole { get; set; } = false;

        protected override void RoleAdded(Player player)
        {
            
            Log.Debug($"{nameof(RoleAdded)}: Added role SCP-600 for {player.DisplayNickname}");
            base.RoleAdded(player);
            player.SessionVariables.Add(apperancekey, StartApperance);
            Timing.RunCoroutine(ApperanceUpdate(player), $"{player.Id}-{nameof(ApperanceUpdate)}");
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
        public override void RemoveRole(Player player)
        {
            Log.Debug($"{nameof(RemoveRole)}: Remove role SCP-600 for {player.DisplayNickname}");
            Timing.KillCoroutines($"{player.Id}-{nameof(ApperanceUpdate)}");
            player.SessionVariables.Remove(apperancekey);
            base.RemoveRole(player);
        }
        // updates scp 600 skin for all players every 15 seconds
        private IEnumerator<float> ApperanceUpdate(Player player)
        {
            for (; ; )
            {
                Log.Debug($"{nameof(ApperanceUpdate)}: Start iteration for the Player.");
                if (player == null) break;
                if (!Check(player) | player.IsDead) break;
                if (player.SessionVariables.TryGetValue(apperancekey, out object val))
                {
                    player.ChangeAppearance((RoleTypeId)val);
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
            Exiled.Events.Handlers.Server.EndingRound -= OnEndingRound;
        }

        private void OnEndingRound(EndingRoundEventArgs e)
        {
            if (e.LeadingTeam == LeadingTeam.Anomalies) return;
            if (TrackedPlayers.Count == 0) return;
            if (Player.List.Any(x => x.IsAlive && x.IsHuman && !Check(x) && x.Role.Type != RoleTypeId.Tutorial)) return;
            e.IsAllowed = false;
        }

        private void OnEntaringHazards(EnteringEnvironmentalHazardEventArgs e)
        {
            if (!IsFrindleFireEnabled && Check(e.Player)) e.IsAllowed = false;
        }

        private void OnHurting(HurtingEventArgs e)
        {
            if (e.Player == null && e.Attacker == null) return;
            if (!IsFrindleFireEnabled && ((Check(e.Player) && e.Attacker.IsScp) || (Check(e.Attacker) && e.Player.IsScp))) e.IsAllowed = false;
        }

        private void OnEnteringPocketDemension(EnteringPocketDimensionEventArgs e)
        {
            if (!IsFrindleFireEnabled && Check(e.Player)) e.IsAllowed = false;
        }

        private void OnDied(DiedEventArgs e)
        {
            if (Check(e.Attacker) && e.Player != e.Attacker)
            {
                ChangeApperance(e.Attacker, e.TargetOldRole);
                if (IsAhpRenerate) e.Attacker.ArtificialHealth += AhpAmount;
                if (IsAhpMaxIncrease) e.Attacker.MaxArtificialHealth += (AhpAmount / 2);
                e.Attacker.ShowHint(KillMessage.Replace("%player%", e.Player.DisplayNickname).Replace("%role%", e.TargetOldRole.ToString()), 4);
                Log.Debug($"{nameof(OnDied)}: {e.Attacker.DisplayNickname}(SCP-600) killed player {e.Player.DisplayNickname}");
            }
            if (Check(e.Player)) // For some reason unknown to me, when a player dies, he has the role of Scp600, according to my role, so we will remove it ourselves:( (However, all abilities disappear as does the event with the player)
            {
                this.RemoveRole(e.Player);
            }
        }

        private void OnPickupingItem(PickingUpItemEventArgs e)
        {
            if (Check(e.Player) && BlackListItems.Contains(e.Pickup.Type)) e.IsAllowed = false;
        }

        public static void ChangeApperance(Player ply, RoleTypeId role)
        {
            if (!RegisteredInstance.Check(ply)) return;
            ply.ChangeAppearance(role);
            ply.SessionVariables[apperancekey] = role;
        }
    }
}