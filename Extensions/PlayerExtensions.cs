using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Roles;
using System.Collections.Generic;

namespace SCP_600V.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// determines whether the player is a custom scp
        /// </summary>
        /// <param name="ply"></param>
        /// <returns>true if is supported custom else false</returns>
        public static bool IsCustomScp(this Player ply)
        {
            //keys for know
            List<string> Keys = new List<string>()
            {
                "IsScp",
                "IsScp600",
                "IsSH",
                "IsScp035"
            };
            foreach (string a in Keys)
            {
                if (ply.SessionVariables.ContainsKey(a))
                {
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// determines whether the player is Scp600
        /// </summary>
        /// <param name="ply"></param>
        /// <returns>true if is have</returns>
        public static bool IsScp600(this Player ply)
        {
            try
            {
                if (ply.SessionVariables.ContainsKey("IsScp600") | CustomRole.Get(typeof(Scp600)).Check(ply))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
