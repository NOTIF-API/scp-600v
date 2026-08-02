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
            RoleTypeId role = Main.Instance.Config.ApperancableRoles.RandomItem();
            Scp600v.ChangeApperance(player, role);
            Main.Instance.Config.AbilityUseMessage.Show(player, (string inner) =>
            {
                return inner.Replace("%role%", role.ToString());
            });
            Log.Debug($"[AbilityUsed]: Invoked ability use for {player.DisplayNickname}\nnew apperace role: {role}");
        }
    }
}