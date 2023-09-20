﻿using Exiled.API.Enums;
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
        // makes sure that the game does not end if there are conditions interfering with the end
        /*internal void OnEndingRound(EndingRoundEventArgs e)
        {
            bool scp = false;
            bool human = false;

            int mtf = Role.AmountTeamNotScp(Team.FoundationForces);
            int d = Role.AmountTeamNotScp(Team.ClassD);
            int s = Role.AmountTeamNotScp(Team.SCPs);

            if (mtf > 0 || d > 0)
            {
                human = true;
            }
            if (s > 0 || Role.Scp600Players().Count() > 0)
            {
                scp = true;
            }
            if (human & scp)
            {
                e.IsRoundEnded = false;
            }
        }*/
        // determines whether a player will be selected for role 600 or not
        internal void OnRoundStarted()
        {
            bool Spawnable = IsSpawnable();
            if (Spawnable & Server.PlayerCount > 2)
            {
                List<Player> players = new List<Player>();
                foreach (Player p in Player.List.Where(x => x.IsAlive && x.Role.Type == RoleTypeId.ClassD))
                {
                    players.Add(p);
                }
                Role.Spawn(players[UnityEngine.Random.Range(1, players.Count())]);
                Log.Debug("Spawned random players");
            }
            else
            {
                return;
            }
        }
        internal bool IsSpawnable()
        {
            if (UnityEngine.Random.value <= Sai.Instance.Config.PercentToSpawn/100)
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
