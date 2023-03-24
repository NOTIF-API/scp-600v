using System;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class EndingRound
    {
        internal void OnEndingRound(EvArg.Server.EndingRoundEventArgs e)
        {
            if (API.Players.Scp600PlyGet.GetScp600().Count != 0&&API.Players.Scp600PlyGet.GetSH().Count != 0&&API.Players.Scp600PlyGet.GetCostumSCP().Count != 0 && API.Players.Scp600PlyGet.GetScp035().Count != 0)
            {
                e.IsRoundEnded = false;
            }
        }
    }
}
