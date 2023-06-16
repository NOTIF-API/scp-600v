using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;

namespace SCP_600V.API.Players
{
    public class Scp600PlyGet
    {
        /// <summary>
        /// Check Player is SCP-600v
        /// </summary>
        /// <param name="player">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsScp600(Player player)
        {
            return player.SessionVariables.ContainsKey("IsSCP600");
        }
        /// <summary>
        /// Check Player is serpents hand
        /// </summary>
        /// <param name="player">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsSH(Player player)
        {
            return player.SessionVariables.ContainsKey("IsSH");
        }
        /// <summary>
        /// Check Player is custom scp
        /// </summary>
        /// <param name="player">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsCustomScp(Player player)
        {
            return player.SessionVariables.ContainsKey("IsScp");
        }
        /// <summary>
        /// Check Player is scp035
        /// </summary>
        /// <param name="player">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsScp035(Player player)
        {
            return player.SessionVariables.ContainsKey("IsScp035");
        }
        /// <summary>
        /// Get player list playing as SCP-600v
        /// </summary>
        /// <returns>Player list is scp600</returns>
        public static List<Player> GetScp600() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsSCP600")).ToList();
        /// <summary>
        /// Get player list playing as serpents hand
        /// </summary>
        /// <returns>Player list is SH</returns>
        public static List<Player> GetSH() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsSH")).ToList();
        /// <summary>
        /// Get player list playing as Scp costum created with sesion variables set as IsScp
        /// </summary>
        /// <returns>Player list is IsScp</returns>
        public static List<Player> GetCostumSCP() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp")).ToList();
        /// <summary>
        /// Get player list playing as scp-035
        /// </summary>
        /// <returns>Player list is IsScp035</returns>
        public static List<Player> GetScp035() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp035")).ToList();

    }
}
