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
        /// <param name="ply">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsScp600(Player ply)
        {
            return ply.SessionVariables.ContainsKey("IsSCP600");
        }
        /// <summary>
        /// Check Player is serpents hand
        /// </summary>
        /// <param name="ply">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsSH(Player ply)
        {
            return ply.SessionVariables.ContainsKey("IsSH");
        }
        /// <summary>
        /// Check Player is custom scp
        /// </summary>
        /// <param name="ply">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsCustomScp(Player ply)
        {
            return ply.SessionVariables.ContainsKey("IsScp");
        }
        /// <summary>
        /// Check Player is scp035
        /// </summary>
        /// <param name="ply">Checked player</param>
        /// <returns>true or false</returns>
        public static bool IsScp035(Player ply)
        {
            return ply.SessionVariables.ContainsKey("IsScp035");
        }
        /// <summary>
        /// Get can scp damage my scp's
        /// </summary>
        /// <returns>true or false</returns>
        public static bool CanScpDamage()
        {
            return Sai.Instance.Config.IsScpCanDamageMe;
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
        /// Get player list playing as Scp costum created
        /// </summary>
        /// <returns>Player list is scp</returns>
        public static List<Player> GetCostumSCP() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp")).ToList();
        /// <summary>
        /// Get player list playing as scp-035
        /// </summary>
        /// <returns>Player list is scp035</returns>
        public static List<Player> GetScp035() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp035")).ToList();

    }
}
