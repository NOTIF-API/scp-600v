using System.Linq;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;

using SCP_600V.Roles;

namespace SCP_600V.API.Extensions
{
    public static class PlayerExtension
    {
        public static void SpawnAs600(this Player player)
        {
            if (player == null) return;
            if (player.IsDead) return;
            try
            {
                CustomRole.Get(typeof(Scp600v)).AddRole(player);
            }
            catch { }
        }
        public static bool IsScp600(this Player player)
        {
            try
            {
                return (player.SessionVariables.ContainsKey("IsScp600") || CustomRole.Get(typeof(Scp600v)).Check(player));
            }
            catch { return false; }
        }
        public static Player[] Scp600(this Player player) => Player.List.Where(x => x.IsScp600()).ToArray();
    }
}
