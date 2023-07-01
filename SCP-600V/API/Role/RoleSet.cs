using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using MEC;
using SCP_600V.Extension;

namespace SCP_600V.API.Role
{
    /// <summary>
    /// allows you to assign the role of SCP-600V to players
    /// </summary>
    public class RoleSet
    {
        /// <summary>
        /// Gives the role of SCP-600V to the player
        /// </summary>
        /// <param name="player">The player who will receive the role of SCP-600V</param>
        public static void Spawn(Player player)
        {
            if (player == null && player.IsDead)
            {
                return;
            }
            else
            {
                CustomRole.Get(typeof(Scp600CotumRoleBase)).AddRole(player);
            }
        }
        /// <summary>
        /// Gives the role of SCP-600V to the player
        /// </summary>
        /// <param name="player">The player who will receive the role of SCP-600V</param>
        /// <param name="MaxHealt">Assigning a custom MaxHealt parameter to a player</param>
        public static void Spawn(Player player, int MaxHealt = 400)
        {
            if (player == null && player.IsDead)
            {
                return;
            }
            else
            {
                CustomRole.Get(typeof(Scp600CotumRoleBase)).AddRole(player);
                Timing.CallDelayed(2f, () =>
                {
                    player.MaxHealth = MaxHealt;
                    player.Health = MaxHealt;
                });
            }
        }
    }
}
