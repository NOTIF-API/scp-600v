using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events.EventArgs
{
    public class DiedEventArg
    {
        public Player Player { get; set; }

        public Player Killer { get; set; }

        public DiedEventArg(Player player, Player killer)
        {
            Player = player;
            Killer = killer;
        }
    }
}
