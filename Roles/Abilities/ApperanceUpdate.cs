using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;

using PlayerRoles;

namespace SCP_600V.Roles.Abilities
{
    public class ApperanceUpdate : ActiveAbility
    {
        public override float Duration { get; set; } = 1;
        public override float Cooldown { get; set; } = 60;
        public override string Name { get; set; } = "Apperance changer";
        public override string Description { get; set; } = "Changes your appearance to a random one";

        protected override void AbilityUsed(Player player)
        {
            RoleTypeId newApperance = Main.ApperaceableRoles.RandomItem();
            Scp600v.ChangeApperance(player, newApperance);
            player.Broadcast(5, Main.Instance.Config.AbilityUseMessage.Replace("%role%", newApperance.ToString()), Broadcast.BroadcastFlags.Normal, false);
            Log.Debug($"{nameof(AbilityUsed)}: Invoked ability used for {player.DisplayNickname}");
        }
    }
}