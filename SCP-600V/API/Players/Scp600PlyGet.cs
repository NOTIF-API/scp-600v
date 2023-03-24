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
        /// Get can scp damage my scp's
        /// </summary>
        /// <returns>true or galse</returns>
        public static bool CanScpDamage()
        {
            return Sai.Instance.Config.IsScpCanDamageMe;
        }
        /// <summary>
        /// Get player list playing as SCP-600v
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetScp600() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsSCP600")).ToList();
        public static List<Player> GetSH() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsSH")).ToList();
        public static List<Player> GetCostumSCP() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp")).ToList();
        public static List<Player> GetScp035() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsScp035")).ToList();
        public static List<Player> GetFullAt() => Player.List.Where(x => x.SessionVariables.ContainsKey("FullAt")).ToList();

    }
}
