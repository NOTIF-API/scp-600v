using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace SCP_600V.API.Role
{
    /// <summary>
    /// allows you to get information on certain criteria about the roles on the server
    /// </summary>
    public class RoleGet
    {
        /// <summary>
        /// gets a list of players with the Breeder role on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> Scp600Players() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsSCP600")).ToList();
        /// <summary>
        /// gets a list of players who have the Serpent's Hand role on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> SHPlayers() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsSH")).ToList();
        /// <summary>
        /// gets a list of players with the role of Mask of Obsession on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> Scp035Players() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsScp035")).ToList();
        /// <summary>
        /// Gets a player and defines their role
        /// </summary>
        /// <param name="player">Player to check</param>
        /// <returns>returns true if the player is SCP-600 or not null</returns>
        public static bool IsScp600(Player player)
        {
            if (player != null & player.SessionVariables.ContainsKey("IsSCP600"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
