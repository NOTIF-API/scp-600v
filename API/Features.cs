using System;
using System.Linq;
using System.Reflection;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;

using SCP_600V.Extensions;
using SCP_600V.Roles;

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
            Log.Debug($"{nameof(SpawnPlayer)}: Called by {Assembly.GetCallingAssembly().GetName().Name} for {ply.Nickname}");
            CustomRole.Get(typeof(Scp600)).AddRole(ply);
        }
        /// <summary>
        /// Lets you get <see cref="Array"/> containing players playing for Scp600
        /// </summary>
        /// <returns>An array with players or null if there is no one</returns>
        public static Player[] GetAffected() => Player.List.Where(x => x != null & x.IsScp600()).ToArray();
        /// <summary>
        /// Return if player is scp600 now, original source <see cref="PlayerExtensions.IsScp600(Player)"/>
        /// </summary>
        /// <param name="ply"><see cref="Player"/> for check</param>
        [Obsolete("there is a method in Extensions which is also called", false)]
        public static bool IsScp600(Player ply) => ply.IsScp600();
    }
}