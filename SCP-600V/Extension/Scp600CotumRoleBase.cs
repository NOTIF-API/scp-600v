using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using PlayerRoles;
using UnityEngine;
using MEC;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using EvHandler = Exiled.Events.Handlers;
using YamlDotNet.Serialization;
using System.ComponentModel;
using Exiled.API.Features.Spawn;

namespace SCP_600V.Extension
{
    [CustomRole(RoleTypeId.Tutorial)]
    internal class Scp600CotumRoleBase : CustomRole
    {
        public override string CustomInfo { get; set; } = "SCP-600V";
        
        public override string Name { get; set; } = "SCP-600V";
        
        public override int MaxHealth { get; set; } = 400;

        [Description("whether it will be shown next to the player's name that he is SCP-600V")]
        public bool VisibleScpName { get; set; } = true;

        [YamlIgnore]
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;

        [Description("the initial appearance of the object during behavior (preferably left as it will follow among class D)")]
        public RoleTypeId StartAppearance { get; set; } = RoleTypeId.ClassD;

        public override string Description { get; set; } = "<color=\"purple\">Help other scp complete task</color>";
        
        public override uint Id { get; set; } = 96;
        
        public override List<string> Inventory { get; set; } = new List<string>() { ItemType.KeycardScientist.ToString()};
        
        public override string ConsoleMessage { get; set; } = "<color=\"green\">Your are spawned as</color> <color=\"red\">SCP-600V</color>";
        [YamlIgnore]
        public RoleTypeId VisibledRole { get; set; }
        [YamlIgnore]
        public override bool KeepPositionOnSpawn { get; set; }
        [YamlIgnore]
        public override bool KeepRoleOnDeath { get; set; } = false;
        [YamlIgnore]
        public override bool KeepRoleOnChangingRole { get; set; } = false;
        [YamlIgnore]
        public override float SpawnChance { get; set; }
        [YamlIgnore]
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();
        [YamlIgnore]
        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>();
        [Description("will allow you to change the size of scp but personally I would not change")]
        public override Vector3 Scale { get; set; } = new Vector3(1, 1, 1);

        public override Dictionary<RoleTypeId, float> CustomRoleFFMultiplier { get; set; } = new Dictionary<RoleTypeId, float>()
        {
            { RoleTypeId.Scp049, 0 },
            { RoleTypeId.Scp173, 0 },
            { RoleTypeId.Scp096, 0 },
            { RoleTypeId.Scp939, 0 },
            { RoleTypeId.Scp0492, 0 },
            { RoleTypeId.Scp106, 0 }
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato9, 30 },
            { AmmoType.Ammo44Cal, 0 },
            { AmmoType.Nato556, 0 },
            { AmmoType.Nato762, 0 },
            { AmmoType.Ammo12Gauge, 0}
        };
        public List<ItemType> DontUserItems { get; private set; } = new List<ItemType>()
        {
            ItemType.MicroHID
        };
        public override void AddRole(Player player)
        {
            Vector3 pos = player.Position;
            if (this.Role != RoleTypeId.None)
            {
                player.Role.Set(this.Role, RoleSpawnFlags.None);
                Timing.CallDelayed(1f, () =>
                {
                    player.Position = pos;
                    player.ClearInventory();
                    foreach (string PlyItem in this.Inventory)
                    {
                        this.TryAddItem(player, PlyItem);
                    }
                    foreach (AmmoType key in this.Ammo.Keys)
                    {
                        player.SetAmmo(key, this.Ammo[key]);
                    }
                    player.Health = (float)this.MaxHealth;
                    player.MaxHealth = (float)this.MaxHealth;
                    if (!player.RemoteAdminAccess | this.VisibleScpName)
                    {
                        player.RankName = "SCP-600V";
                        player.RankColor = Sai.Instance.Config.BadgeColor;
                    }
                    this.ShowMessage(player);
                    this.TrackedPlayers.Add(player);
                    this.RoleAdded(player);
                    player.UniqueRole = this.Name;
                    player.TryAddCustomRoleFriendlyFire(this.Name, this.CustomRoleFFMultiplier);
                    player.Scale = this.Scale;
                    if (this.VisibleScpName)
                    {
                        player.CustomInfo = $"{player.Nickname}\n{this.CustomInfo}";
                        player.InfoArea &= ~PlayerInfoArea.Role;
                    }
                    if (Sai.Instance.Config.CanBleading)
                    {
                        Timing.RunCoroutine(this.Hurting(player), $"{player.Id}-hurt");
                    }
                    try
                    {
                        player.SessionVariables.Add("IsSCP600", null);
                        player.SessionVariables.Add("IsScp", null);
                    }
                    catch
                    {

                    }
                });
            }
        }
        protected override void RoleAdded(Player player)
        {
            Timing.CallDelayed(1f, () =>
            {
                player.ChangeAppearance(this.StartAppearance, false, 0);
                this.VisibledRole = RoleTypeId.ClassD;
            });
            base.RoleAdded(player);
        }
        protected override void RoleRemoved(Player player)
        {
            Log.Debug($"SCP-600V dead and role romoved to player: {player}");
            try
            {
                player.SessionVariables.Remove("IsSCP600");
                player.SessionVariables.Remove("IsScp");
                Timing.KillCoroutines($"{player.Id}-hurt");
                if (!player.RemoteAdminAccess)
                {
                    player.RankName = string.Empty;
                    player.RankColor = string.Empty;
                }
            }
            catch
            {
                
            }
            base.RoleRemoved(player);
        }
        protected override void SubscribeEvents()
        {
            EvHandler.Player.Died += this.OnDeath;
            EvHandler.Player.PickingUpItem += this.PickUpItems;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            EvHandler.Player.Died -= this.OnDeath;
            EvHandler.Player.PickingUpItem -= this.PickUpItems;
            base.UnsubscribeEvents();
        }
        public void OnDeath(DiedEventArgs e)
        {
            if (Check(e.Attacker) & e.Attacker != null & e.Player != null & e.Attacker != e.Player)
            {
                Debug.Log("Attacker is SCP600");
                if (e.TargetOldRole != this.VisibledRole & e.TargetOldRole != RoleTypeId.None & e.TargetOldRole != RoleTypeId.Spectator)
                {
                    this.VisibledRole = e.TargetOldRole;
                    e.Attacker.ChangeAppearance(this.VisibledRole, false, 0);
                }
            }
        }
        public void PickUpItems(PickingUpItemEventArgs e)
        {
            if (e.Player != null & Check(e.Player) & this.DontUserItems.Contains(e.Pickup.Type))
            {
                e.IsAllowed = false;
            }
        }
        public IEnumerator<float> Hurting(Player player)
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(5);
                player.Hurt(5f, DamageType.Bleeding);
            }
        }
    }
}
