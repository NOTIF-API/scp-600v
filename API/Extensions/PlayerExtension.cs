using System;

using Exiled.API.Features;
using Exiled.CustomRoles.API;

using SCP_600V.Roles;

namespace SCP_600V.API.Extensions
{
    public static class PlayerExtension
    {
        public static void SpawnAs600(this Player player)
        {
            if (player == null) return;
            if (player.HasAnyCustomRole()) return;
            Scp600v.RegisteredInstance?.AddRole(player);
        }
        public static bool IsScp600(this Player player)
        {
            try
            {
                return Main.Instance.Config.ScpRole.Check(player);
            }
            catch (Exception ex)
            {
                Log.Debug($"{nameof(IsScp600)}: {ex.Message}");
                return false; 
            }
        }
    }
}
