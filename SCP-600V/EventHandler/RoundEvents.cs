using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using SCP_600V.API.Role;
using System.Collections.Generic;

namespace SCP_600V.EventHandler
{
    internal class RoundEvents
    {
        internal void OnEndingRound(EndingRoundEventArgs e)
        {
            bool scp = false;
            bool human = false;

            int mtf = TeamGet.AmountTeamNotScp(Team.FoundationForces);
            int d = TeamGet.AmountTeamNotScp(Team.ClassD);
            int s = TeamGet.AmountTeamNotScp(Team.SCPs);

            if (mtf > 0 || d > 0)
            {
                human = true;
            }
            if (s > 0 || RoleGet.Scp600Players().Count() > 0)
            {
                scp = true;
            }
            if (human & scp)
            {
                e.IsRoundEnded = false;
            }
        }
        internal void OnRoundStarted()
        {
            bool Spawnable = IsSpawnable();
            if (Spawnable && Server.PlayerCount > 2)
            {
                List<Player> players = new List<Player>();
                foreach (Player p in Player.List.Where(x => x.IsAlive && x.Role.Type == RoleTypeId.ClassD))
                {
                    players.Add(p);
                }
                RoleSet.Spawn(players[UnityEngine.Random.Range(1, players.Count())]);
                Log.Debug("Spawned random players");
            }
            else
            {
                return;
            }
        }
        internal bool IsSpawnable()
        {
            if (UnityEngine.Random.value <= Sai.Instance.Config.PercentToSpawn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
