using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SCP_600V.API.Role;

namespace SCP_600V.EventHandler
{
    internal class GameEvents
    {
        internal void OnEscape(EscapingEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug($"Canceling escape scp600 player: [{e.Player.Nickname}]");
                e.IsAllowed = false;
            }
        }
        internal void ChangingRole(ChangingRoleEventArgs e)
        {
            if (e.Player != null && e.Reason == SpawnReason.ForceClass & e.NewRole == RoleTypeId.None || e.NewRole == RoleTypeId.Spectator & RoleGet.IsScp600(e.Player))
            {
                Log.Debug("Detected admin forces scp600 to spectator");
                e.Player.ShowHint("\n\n\n\n\n\n<color=\"red\">Your are admin forced to spectator scp600 is removed role for your");
            }
        }
        internal void EnetignPocketDemens(EnteringPocketDimensionEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug("Scp-106 don't touch scp600 in pocket demension");
                e.IsAllowed = false;
            }
        }
        /// <summary>
        /// added to prevent scp from killing each other
        /// </summary>
        internal void HurtingScp(HurtingEventArgs e)
        {
            if (e.Player != null & RoleGet.IsScp600(e.Player) & e.Attacker.Role.Team == Team.SCPs)
            {
                e.IsAllowed = false;
            }
            else
            {
                return;
            }
        }
    }
}
