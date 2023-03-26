using System;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.Collections.Generic;
using PlayerRoles;
using System.Linq;

namespace SCP_600V.API.Players
{
    public class Servroles
    {
        /// <summary>
        /// get player as human not scp's
        /// </summary>
        /// <returns></returns>
        public int GetAllPlayerAsNotScp()
        {
            int a = 0;
            foreach (Player b in Player.List)
            {
                if (!Scp600PlyGet.IsScp600(b))
                {
                    if (!Scp600PlyGet.IsSH(b))
                    {
                        if (!b.IsScp)
                        {
                            a += 1;
                        }
                    }
                }
            }
            return a;
        }
        /// <summary>
        /// Get amount user per team
        /// </summary>
        /// <param name="team">Exiled.API.Enums.Team</param>
        /// <returns>int number</returns>
        public static int TeamsUser(Team team) => Player.List.Where(x => x.Role.Team == team&& !x.SessionVariables.ContainsKey("IsSCP600")).Count();
        /// <summary>
        /// Get amount user per costum role by session variable key
        /// </summary>
        /// <param name="keyrole">string key</param>
        /// <returns>int number</returns>
        public static int RolesUser(string keyrole) => Player.List.Where(x => x.SessionVariables.ContainsKey((string)keyrole)).Count();
    }
}
