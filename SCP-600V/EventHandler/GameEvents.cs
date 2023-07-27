using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SCP_600V.API.Role;

namespace SCP_600V.EventHandler
{
    internal class GameEvents
    {
        // does not allow escape for 600
        internal void OnEscape(EscapingEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug($"Canceling escape scp600 player: [{e.Player.Nickname}]");
                e.IsAllowed = false;
            }
        }
        // does not allow 106 to catch 600 in its dimension
        internal void EnetignPocketDemens(EnteringPocketDimensionEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug("Scp-106 don't touch scp600 in pocket demension");
                e.IsAllowed = false;
            }
        }
        // making sure SCPs can't kill 600
        internal void HurtingPlayer(HurtingEventArgs e)
        {
            if (e.Player != null & e.Attacker != null & RoleGet.IsScp600(e.Player) & e.Attacker.Role.Team == Team.SCPs)
            {
                e.IsAllowed = false;
            }
        }
    }
}
