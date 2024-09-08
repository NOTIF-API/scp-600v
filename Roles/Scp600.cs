using Exiled.API.Features;
using Exiled.API.Features.Hazards;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using SCP_600V.Events;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SCP_600V.Extensions;
using SCP_600V.Events.EventArg;

namespace SCP_600V.Roles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class Scp600 : CustomRole
    {
        public override uint Id { get; set; } = 600;
        public override int MaxHealth { get; set; } = 400;
        public override string Name { get; set; } = "SCP-600V";
        public override string Description { get; set; } = "Angry scp 600, help other scp complete a task";

        [Description("If need hide set a empty string")]
        public override string CustomInfo { get; set; } = "SCP-600V";
        [Description("Role a player self visible (do not change to scp's)")]
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        [Description("Can scp600 get damage")]
        public bool CanBleading { get; set; } = true;
        [Description("Chance for spawn role")]
        public override float SpawnChance { get; set; } = 15;
        [Description("The message that the player will see when he kills the player")]
        public string TransformationMessage { get; set; } = "you killed %player% and changed your apperance to %role%";
        [Description("Will the player receive AHP when killing another player")]
        public bool AddAhpWhenKill { get; set;} = true;
        [Description("The amount a player will receive when he kills a player")]
        public int AhpAmount { get; set; } = 15;
        [Description("Will the player be affected by the effects of various scp (for example, invisible field 939)")]
        public bool ScpAffectPlayer { get; set; } = false;

        public override bool IgnoreSpawnSystem { get; set; } = true;

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
            RespawningEventArgs e = new RespawningEventArgs(player);
            EventManager.InvokeRespawning(e);
            Log.Debug($"{e.IsAllow} for event scp600 spawning detected");
            if (e.IsAllow)
            {
                Log.Debug($"adding role for {player.Nickname}");
                base.AddRole(player);
            }
        }
        protected override void RoleAdded(Player player)
        {
            RespawnedEventArgs e = new RespawnedEventArgs(player);
            EventManager.InvokeRespawned(e);
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
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard += this.OnEnterEnviromentHazard;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += this.OnEnteringPocketDemension;
            Exiled.Events.Handlers.Scp096.AddingTarget += this.OnAddingTarget;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= this.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem -= this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died -= this.OnDied;
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard -= this.OnEnterEnviromentHazard;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= this.OnEnteringPocketDemension;
            Exiled.Events.Handlers.Scp096.AddingTarget -= this.OnAddingTarget;
            base.UnsubscribeEvents();
        }

        private void OnDied(DiedEventArgs e)
        {
            if (e.Player == null | e.Attacker == null) return;
            if (this.Check(e.Player))
            {
                KilledEventArgs ev = new KilledEventArgs(e.Player, e.Attacker);
                EventManager.InvokeKilled(ev);
                return;
            }
            else if (this.Check(e.Attacker) & this.AddAhpWhenKill)
            {
                e.Attacker.ArtificialHealth += this.AhpAmount;
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
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        private void OnHurting(HurtingEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Log.Debug($"{nameof(OnHurting)} error {ex.Message}");
            }
        }
        // scp173, scp106, scp939 handler hazard
        private void OnEnterEnviromentHazard(EnteringEnvironmentalHazardEventArgs e)
        {
            if (!this.ScpAffectPlayer & Check(e.Player))
            {
                if (e.Hazard is AmnesticCloudHazard | e.Hazard is TantrumHazard | e.Hazard is SinkholeHazard)
                {
                    e.IsAllowed = false;
                    return;
                }
            }
        }
        // scp106 handler
        private void OnEnteringPocketDemension(EnteringPocketDimensionEventArgs e)
        {
            if (!this.ScpAffectPlayer && Check(e.Player))
            {
                e.IsAllowed = false;
            }
        }
        // Scp096 handler
        private void OnAddingTarget(AddingTargetEventArgs e)
        {
            if (!this.ScpAffectPlayer && Check(e.Player))
            {
                e.IsAllowed = false;
            }
        }
        private IEnumerator<float> Hurting(Player player)
        {
            for (; ; )
            {
                if (player == null | !player.IsConnected)
                {
                    Log.Debug($"{nameof(Hurting)} Hurting coroutine stopped, player is null or not connected");
                    break;
                }

                int damage = UnityEngine.Random.Range(1, 5);
                yield return Timing.WaitForSeconds(5);
                player.Hurt(damage, DamageType.Bleeding);
                Log.Debug($"{nameof(Hurting)} {player.Nickname} hurted {damage} hp");
            }
        }
    }
}
