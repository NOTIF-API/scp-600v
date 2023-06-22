using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API.Features;
using Item = Exiled.API.Features.Items;
using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using PlayerRoles;
using UnityEngine;
using MEC;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using EvHandler = Exiled.Events.Handlers;

namespace SCP_600V.Extension
{
    [CustomRole(PlayerRoles.RoleTypeId.Tutorial)]
    internal class Scp600CotumRoleBase : CustomRole
    {
        public override string CustomInfo { get; set; } = "SCP-600V";
        public override string Name { get; set; } = "SCP-600V";
        public override int MaxHealth { get; set; } = 400;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public override string Description { get; set; } = "<color=\"purple\">Help other scp complete task</color>";
        public override uint Id { get; set; } = 96;
        public override List<string> Inventory { get; set; } = new List<string>() { ItemType.KeycardScientist.ToString()};
        public override string ConsoleMessage { get; set; } = "<color=\"green\">Your are spawned as</color> <color=\"red\">SCP-600V</color>";
        private RoleTypeId VisibledRole { get; set; }
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
                    if (!player.RemoteAdminAccess)
                    {
                        player.RankName = "SCP-600V";
                        player.RankColor = Sai.Instance.Config.BadgeColor;
                    }
                    this.ShowMessage(player);
                    this.TrackedPlayers.Add(player);
                    this.RoleAdded(player);
                    player.UniqueRole = this.Name;
                    player.TryAddCustomRoleFriendlyFire(this.Name, this.CustomRoleFFMultiplier);

                    player.CustomInfo = $"{player.Nickname}\n{this.CustomInfo}";
                    player.InfoArea &= ~PlayerInfoArea.Role;

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
            base.RoleAdded(player);
        }
        protected override void RoleAdded(Player player)
        {
            Timing.CallDelayed(1f, () =>
            {
                player.ChangeAppearance(RoleTypeId.ClassD, false, 0);
                this.VisibledRole = RoleTypeId.ClassD;
            });
            base.RoleRemoved(player);
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
            if (e.Attacker != null & e.Player != null & e.Attacker != e.Player)
            {
                if (Check(e.Attacker))
                {
                    Debug.Log("Attacker is SCP600");
                    if (e.TargetOldRole != RoleTypeId.None & e.TargetOldRole != RoleTypeId.Spectator& e.TargetOldRole != this.VisibledRole)
                    {
                        e.Attacker.ChangeAppearance(e.TargetOldRole, false, 0);
                        this.VisibledRole = e.TargetOldRole;
                    }
                }
            }
        }
        public void PickUpItems(PickingUpItemEventArgs e)
        {
            if (this.DontUserItems.Contains(e.Pickup.Type) & e.Player != null & Check(e.Player))
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
