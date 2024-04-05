using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extensions;
using SCP_600V.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.API
{
    public class Features
    {
        /// <summary>
        /// allows you to give the Scp600 role to a player without any special implementation into the project and searching for a registered role by its ID, etc.
        /// </summary>
        /// <param name="ply">Player who getted role</param>
        public static void SpawnPlayer(Player ply)
        {
            CustomRole.Get(typeof(Scp600)).AddRole(ply);
        }
        /// <summary>
        /// Lets you get <see cref="Array"/> containing players playing for Scp600
        /// </summary>
        /// <returns>An array with players or null if there is no one</returns>
        public static Player[] GetAffected()
        {
            Player[] a = Player.List.Where(x => x != null & x.IsScp600()).ToArray();
            if (a.Length == 0) return null;
            else
            {
                return a;
            }
        }
        /// <summary>
        /// Is the player Scp600
        /// </summary>
        /// <param name="ply"></param>
        /// <returns>true if scp600</returns>
        [Obsolete("there is a method in Extensions which is also called", false)]
        public static bool IsScp600(Player ply) => ply.IsScp600();
    }
}
