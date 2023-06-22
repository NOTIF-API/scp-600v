using System;
using Exiled.API.Features;
using System.Collections.Generic;
using MEC;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extension;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class StartingRound
    {
        internal void OnRoundStarted()
        {
            List<Player> players = new List<Player>();
            Random random = new Random();
            foreach (Player p in Player.List)
            { 
                if (p.IsAlive&&p.Role.Type == PlayerRoles.RoleTypeId.ClassD)
                {
                    players.Add(p);
                }
            }
            bool Siusiu = IsSpawnable();
            if (Siusiu & Server.PlayerCount > 2)
            {
                Log.Debug("Get Random player and spawn as scp-600v");
                int LuckyPlayer = random.Next(players.Count);
                Player pd = players[LuckyPlayer];
                CustomRole.Get(typeof(Scp600CotumRoleBase)).AddRole(pd);
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
