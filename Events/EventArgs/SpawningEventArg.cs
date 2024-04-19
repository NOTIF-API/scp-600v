using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events.EventArgs
{
    public class SpawningEventArg: System.EventArgs
    {
        public Player Player { get; set; }

        public float MaxHealt { get; }

        public bool IsAllow { get; set; }

        public SpawningEventArg(Player player, float maxHealt, bool isAllow)
        {
            Player = player;
            MaxHealt = maxHealt;
            IsAllow = isAllow;
        }
    }
}
