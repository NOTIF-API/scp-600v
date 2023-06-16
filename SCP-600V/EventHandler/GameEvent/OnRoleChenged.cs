using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Enums;
using EvArg = Exiled.Events.EventArgs;
using apiplay = SCP_600V.API.Players;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnRoleChenged
    {
        public void OnRoleChenge(EvArg.Player.ChangingRoleEventArgs e)
        {
            if (e.Player != null)
            {
                if (apiplay.Scp600PlyGet.IsScp600(e.Player)&e.Reason == SpawnReason.ForceClass&e.NewRole == PlayerRoles.RoleTypeId.Spectator)
                {
                    Log.Debug("Detected admin forces scp600 to spectator");
                    e.Player.ShowHint("\n\n\n\n\n\n<color=\"red\">Your are admin forced to spectator scp600 is removed role for your");
                }
            }
        }
    }
}
