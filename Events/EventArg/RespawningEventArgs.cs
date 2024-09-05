using Exiled.API.Features;
using System;

namespace SCP_600V.Events.EventArg
{
    /// <summary>
    /// Event containing information about the future occurrence of an object
    /// </summary>
    public class RespawningEventArgs: EventArgs
    {
        /// <summary>
        /// Player who get a role
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Will the player be allowed to spawn?
        /// </summary>
        public bool IsAllow { get; set; }

        public RespawningEventArgs(Player player, bool isAllow = true)
        {
            Player = player;
            IsAllow = isAllow;
        }
    }
}
