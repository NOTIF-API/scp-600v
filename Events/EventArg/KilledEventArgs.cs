using Exiled.API.Features;
using System;
using System.IO;

namespace SCP_600V.Events.EventArg
{
    /// <summary>
    /// Event containing information and death of an object
    /// </summary>
    public class KilledEventArgs: EventArgs
    {
        /// <summary>
        /// Player who died while playing SCP-600
        /// </summary>
        public Player Scp { get; private set; }
        /// <summary>
        /// Player <see cref="Scp"/> killer
        /// </summary>
        public Player Killer { get; private set; }

        public KilledEventArgs(Player Scp, Player Killer)
        {
            this.Scp = Scp;
            this.Killer = Killer;
        }
    }
}
