using Exiled.API.Features;
using Exiled.API.Enums;
using EvArg = Exiled.Events.EventArgs;
using SCP_600V.API.Role;
using PlayerRoles;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnRoleChenged
    {
        internal void OnRoleChenge(EvArg.Player.ChangingRoleEventArgs e)
        {
            if (e.Player != null && e.Reason == SpawnReason.ForceClass & e.NewRole == RoleTypeId.None || e.NewRole == RoleTypeId.Spectator & RoleGet.IsScp600(e.Player))
            {
                Log.Debug("Detected admin forces scp600 to spectator");
                e.Player.ShowHint("\n\n\n\n\n\n<color=\"red\">Your are admin forced to spectator scp600 is removed role for your");
            }
        }
    }
}
