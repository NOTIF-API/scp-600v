using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using s6 = SCP_600V.Events.Handlers;
using MEC;
using PlayerRoles;
using System;
using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs.Player;
using SCP_600V.Extensions;
using Exiled.API.Extensions;
using System.Threading;
using PlayerStatsSystem;
using SCP_600V.Events.EventArgs;

namespace SCP_600V.Roles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class Scp600 : CustomRole
    {
        public override uint Id { get; set; } = 600;
        public override int MaxHealth { get; set; } = 400;
        public override string Name { get; set; } = "SCP-600V";
        public override string Description { get; set; } = "Angry scp 600, help other scp complete a task";
        [Description("If need hide set a epty string")]
        public override string CustomInfo { get; set; } = "SCP-600V";
        [Description("Role a player self visible (do not change to scp's)")]
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        [Description("Can scp600 get damage")]
        public bool CanBleading { get; set; } = true;

        public override float SpawnChance { get; set; } = 15;

        public string TransformationMessage { get; set; } = "you kelled %player% and changed your apperance to %role%";

        private s6.Scp600Handler Evs6 { get; set; }

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoleSpawnPoints = new List<RoleSpawnPoint>()
            {
                new RoleSpawnPoint() {
                    Chance = 100,
                    Role = RoleTypeId.ClassD
                }
            }
        };

        public List<ItemType> BlackListItems { get; set; } = new List<ItemType>()
        {
            ItemType.MicroHID,
        };

        public override void AddRole(Player player)
        {
            Evs6 = new s6.Scp600Handler();
            SpawningEventArg e = new SpawningEventArg(player, this.MaxHealth, true);
            Evs6.CallSpawning(e);
            Log.Debug($"{e.IsAllow} for event scp600 spawning detected");
            if (e.IsAllow)
            {
                Log.Debug($"adding role for {player.Nickname}");
                base.AddRole(player);
            }
        }
        protected override void RoleAdded(Player player)
        {
            player.SessionVariables.Add("IsScp", null);
            player.SessionVariables.Add("IsScp600", null);
            if (CanBleading)
            {
                Timing.RunCoroutine(this.Hurting(player), $"{player.Id}-hurtingsp6");
            }
            player.ChangeAppearance(RoleTypeId.ClassD);
            base.RoleAdded(player);
        }

        protected override void RoleRemoved(Player player)
        {
            player.SessionVariables.Remove("IsScp600");
            player.SessionVariables.Remove("IsScp");
            Timing.KillCoroutines($"{player.Id}-hurtingsp6");
            base.RoleRemoved(player);
        }
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += this.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem += this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died += this.OnDied;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= this.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem -= this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died -= this.OnDied;
            base.UnsubscribeEvents();
        }

        private void OnDied(DiedEventArgs e)
        {
            if (e.Player == null | e.Attacker == null) return;
            if (this.Check(e.Player))
            {
                DiedEventArg es = new DiedEventArg(e.Player, e.Attacker);
                Evs6.CallDied(es);
                return;
            }
            else if (this.Check(e.Attacker))
            {
                e.Attacker.ArtificialHealth += 15;
                e.Attacker.ChangeAppearance(e.TargetOldRole);
                e.Attacker.ShowHint(this.TransformationMessage.Replace("%player%", e.Player.Nickname).Replace("%role%", e.TargetOldRole.ToString()));
                return;
            }
        }

        private void OnPickingUpItem(PickingUpItemEventArgs e)
        {
            if (e.Player == null | !this.Check(e.Player)) return;
            else
            {
                if (this.BlackListItems.Contains(e.Pickup.Type))
                {
                    e.IsAllowed = false;
                }
                else
                {
                    return;
                }
            }
        }
        private void OnHurting(HurtingEventArgs e)
        {
            if (e.Player == null | e.Attacker == null) return;
            if (this.Check(e.Player))
            {
                if (e.Attacker.IsScp | e.Attacker.IsCustomScp())
                {
                    e.IsAllowed = false;
                    return;
                }
            }
            if (this.Check(e.Attacker))
            {
                if (e.Player.IsCustomScp() | e.Player.IsScp)
                {
                    e.IsAllowed = false;
                    return;
                }
            }
        }

        private IEnumerator<float> Hurting(Player player)
        {
            Random rnd = new System.Random();
            for (; ; )
            {
                if (player == null)
                {
                    Log.Debug("hurt coro stopped, player is null");
                    break;
                }
                int damage = rnd.Next(1, 5);
                yield return Timing.WaitForSeconds(5);
                player.Hurt(damage, DamageType.Bleeding);
                Log.Debug($"{player.Nickname} hurted {damage} hp");
            }
        }
    }
}
