using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using SCP_600V.API.Role;
using System.Collections.Generic;
using System;

namespace SCP_600V.EventHandler
{
    internal class RoundEvents
    {
        // makes sure that the game does not end if there are conditions interfering with the end
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
        // determines whether a player will be selected for role 600 or not
        internal void OnRoundStarted()
        {
            bool Spawnable = IsSpawnable();
            if (Spawnable & Server.PlayerCount >= Sai.Instance.Config.MinimalPlayers)
            {
                List<Player> players = new List<Player>();
                foreach (Player p in Player.List.Where(x => x.IsAlive && x.Role.Type == RoleTypeId.ClassD))
                {
                    players.Add(p);
                }
                Random rnd = new Random();
                RoleSet.Spawn(players[rnd.Next(1, players.Count())]);
                Log.Debug("Spawned random players");
            }
            else
            {
                return;
            }
        }
        internal bool IsSpawnable()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int res = rand.Next(101);
            if (res <= Sai.Instance.Config.PercentToSpawn) return true; else return false;
        }
    }
}
