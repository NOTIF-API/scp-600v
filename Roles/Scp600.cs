using System.Collections.Generic;
using System.ComponentModel;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;

using MEC;

using PlayerRoles;

using YamlDotNet.Serialization;

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
        [YamlIgnore]
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
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
            RoleSpawnPoints = new List<RoleSpawnPoint>()
            {
                new RoleSpawnPoint() {
                    Chance = 45,
                    Role = RoleTypeId.ClassD
                }
            }
        };

        public List<ItemType> BlackListItems { get; set; } = new List<ItemType>()
        {
            ItemType.MicroHID,
            ItemType.Jailbird
        };
        protected override void RoleAdded(Player player)
        {
            player.SessionVariables.Add("IsScp", true);
            player.SessionVariables.Add("IsScp600", null);
            if (CanBleading)
            {
                Timing.RunCoroutine(this.Hurting(player), $"{player.Id}-hurtscp600");
            }
            player.ChangeAppearance(RoleTypeId.ClassD);
            base.RoleAdded(player);
        }

        protected override void RoleRemoved(Player player)
        {
            player.SessionVariables.Remove("IsScp600");
            player.SessionVariables.Remove("IsScp");
            Timing.KillCoroutines($"{player.Id}-hurtscp600");
            base.RoleRemoved(player);
        }
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += this.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem += this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died += this.OnDied;
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard += this.OnEnterEnviromentHazard;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += this.OnEnteringPocketDemension;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= this.OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem -= this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died -= this.OnDied;
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard -= this.OnEnterEnviromentHazard;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= this.OnEnteringPocketDemension;
            base.UnsubscribeEvents();
        }

        private void OnDied(DiedEventArgs e)
        {
            if (e.Player == null | e.Attacker == null) return;
            if (this.Check(e.Attacker))
            {
                e.Attacker.ArtificialHealth += AddAhpWhenKill ? this.AhpAmount : 0;
                e.Attacker.ChangeAppearance(e.TargetOldRole);
                e.Attacker.ShowHint(this.TransformationMessage.Replace("%player%", e.Player.Nickname).Replace("%role%", e.TargetOldRole.ToString()));
                return;
            }
        }

        private void OnPickingUpItem(PickingUpItemEventArgs e)
        {
            if (e.Player == null | Check(e.Player)) return;
            if (!BlackListItems.Contains(e.Pickup.Type)) return;
            e.IsAllowed = false;
            return;
        }
        private void OnHurting(HurtingEventArgs e)
        {
            if (e.Attacker == null | e.Player == null | !Check(e.Player)) return;// players not null, player is not custom object
            if (e.Attacker.Role.Side == Side.Scp & !Main.Instance.Config.ScpConfig.ScpAffectPlayer) // if Scp object can't interact bad with custom object
            {
                e.IsAllowed = false;
            }
        }
        // scp173, scp106, scp939 handler hazard
        private void OnEnterEnviromentHazard(EnteringEnvironmentalHazardEventArgs e)
        {
            if (!this.ScpAffectPlayer & Check(e.Player))
            {
                if (e.Hazard.Type == HazardType.AmnesticCloud | e.Hazard.Type == HazardType.Sinkhole | e.Hazard.Type == HazardType.Tantrum)
                {
                    e.IsAllowed = false;
                }
            }
        }
        // scp106 handler
        private void OnEnteringPocketDemension(EnteringPocketDimensionEventArgs e)
        {
            if (!this.ScpAffectPlayer && Check(e.Player))
            {
                Log.Debug($"{nameof(OnEnteringPocketDemension)}: Intercept the arrival event in the pocket dimension to cancel it taking into account the detection of Scp 600");
                e.IsAllowed = false;
            }
        }
        private IEnumerator<float> Hurting(Player player)
        {
            for (; ; )
            {
                if (player == null | !player.IsConnected)
                {
                    Log.Debug($"{nameof(Hurting)}: Hurting coroutine stopped, player is null or not connected");
                    yield break;
                }
                int damage = UnityEngine.Random.Range(1, 5);
                player.Hurt(damage, DamageType.Bleeding);
                Log.Debug($"{nameof(Hurting)}: {player.Nickname} take: -{damage} hp");
                yield return Timing.WaitForSeconds(5);
            }
        }
    }
}