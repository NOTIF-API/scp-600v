using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Roles;
using System;
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
        [Obsolete("Now roles do not use special keys supported if plugin version oldest", false)]
        public static bool IsCustomScp(this Player ply)
        {
            //keys for know
            List<string> Keys = new List<string>()
            {
                "IsScp",
                "IsScp600",
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
            catch (Exception ex)
            {
                Log.Debug($"{nameof(IsScp600)} returned false with error: {ex.Message}");
                return false;
            }
        }
    }
}
