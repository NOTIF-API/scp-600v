using System;
using Exiled.API.Features;
using Scp = SCP_600V.Extension.Scp600;
using System.Collections.Generic;
using MEC;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class StartingRound
    {
        public void OnRoundStarted()
        {
            if (Sai.Instance.Config.IsFFEnabled == true)
            {
                if (Server.FriendlyFire == false)
                {
                    Server.FriendlyFire = true;
                }
            }
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
            if (Siusiu)
            {
                int LuckyPlayer = random.Next(players.Count);
                Player pd = players[LuckyPlayer];
                Scp ss = new Scp(pd, Sai.Instance.Config.Maxhealt);
            }
        }
        public bool IsSpawnable()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int res = rand.Next(101);
            if (res <= Sai.Instance.Config.PercentToSpawn) return true; else return false;
        }
    }
}
