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
            if (SCP_600V.API.Players.Scp600PlyGet.IsScp600(e.Player))
            {
                if (e.NewRole == PlayerRoles.RoleTypeId.Spectator)
                {
                    
                }
            }
        }
    }
}
