using Exiled.API.Features;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SCP_600V.Events.EventArg
{
    public class SpawningEventArgs: EventArgs
    {
        /// <summary>
        /// player who is scp 600
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Will the player appear at all? (default true)
        /// </summary>
        public bool IsAllow { get; set; } = true;

        public SpawningEventArgs(Player player, bool IsAllow)
        {
            Player = player;
            this.IsAllow = IsAllow;
        }
    }
}
