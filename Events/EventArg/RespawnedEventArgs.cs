using Exiled.API.Features;
using System;

namespace SCP_600V.Events.EventArg
{
    /// <summary>
    /// Information about the already appeared player for the object
    /// </summary>
    public class RespawnedEventArgs: EventArgs
    {
        /// <summary>
        /// Player who getted role
        /// </summary>
        public Player Player { get; private set; }

        public RespawnedEventArgs(Player player) {  Player = player; }
    }
}
