using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;
using EvArg = Exiled.Events.EventArgs;
using PlayerRoles;
using servrol = SCP_600V.API.Players.Servroles;
using splyg = SCP_600V.API.Players.Scp600PlyGet;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class EndingRound
    {
        internal void OnEndingRound(EvArg.Server.EndingRoundEventArgs e)
        {
            bool scp = false;
            bool human = false;

            int mtf = servrol.TeamsUser(Team.FoundationForces);
            int d = servrol.TeamsUser(Team.ClassD);
            int s = servrol.TeamsUser(Team.SCPs);

            if (mtf > 0|| d > 0) 
            {
                human = true;
            }
            if (s > 0 || splyg.GetScp600().Count() > 0)
            {
                scp = true;
            }
            if (human&scp)
            {
                e.IsRoundEnded = false;
            }
        }
    }
}
