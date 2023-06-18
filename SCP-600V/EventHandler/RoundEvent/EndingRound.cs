using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;
using EvArg = Exiled.Events.EventArgs;
using PlayerRoles;
using SCP_600V.API.Role;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class EndingRound
    {
        internal void OnEndingRound(EvArg.Server.EndingRoundEventArgs e)
        {
            bool scp = false;
            bool human = false;

            int mtf = TeamGet.AmountTeamNotScp(Team.FoundationForces);
            int d = TeamGet.AmountTeamNotScp(Team.ClassD);
            int s = TeamGet.AmountTeamNotScp(Team.SCPs);

            if (mtf > 0|| d > 0) 
            {
                human = true;
            }
            if (s > 0 || RoleGet.Scp600Players().Count() > 0)
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
