using System;
using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using SCP_600V.API.Role;
using UnityEngine;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class StartingRound
    {
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
