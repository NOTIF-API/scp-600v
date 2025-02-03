using System;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;

using SCP_600V.Roles;

namespace SCP_600V.Extensions
{
    public static class PlayerExtensions
    {
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
                Log.Debug($"{nameof(IsScp600)}: Returned false with error: {ex.Message}");
                return false;
            }
        }
    }
}
