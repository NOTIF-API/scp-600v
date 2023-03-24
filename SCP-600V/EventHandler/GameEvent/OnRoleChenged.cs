using System;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnRoleChenged
    {
        public void OnRoleChenge(EvArg.Player.ChangingRoleEventArgs e)
        {
            if (e.Player.SessionVariables.ContainsKey("IsSCP600"))
            {
                if (e.NewRole == PlayerRoles.RoleTypeId.Spectator)
                {
                    e.Player.SessionVariables.Remove("IsSCP600");
                    e.Player.SessionVariables.Remove("IsScp");
                }
            }
        }
    }
}
