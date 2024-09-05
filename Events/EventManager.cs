using Exiled.API.Features;
using SCP_600V.Events.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events
{
    internal class EventManager
    {
        public delegate void Killed(KilledEventArgs e);
        public delegate void Respawning(RespawningEventArgs e);
        public delegate void Respawned(RespawnedEventArgs e);
        /// <summary>
        /// Called when the Scp-600 player is killed by another player
        /// </summary>
        public static event Killed OnKilled;
        /// <summary>
        /// Called before Scp600 spawns
        /// </summary>
        public static event Respawning OnRespawning;
        /// <summary>
        /// Called after Scp600 spawns
        /// </summary>
        public static event Respawned OnRespawned;

        internal static void InvokeKilled(KilledEventArgs e)
        {
            OnKilled?.Invoke(e);
            Log.Debug($"{nameof(InvokeKilled)} called");
        }
        internal static void InvokeRespawning(RespawningEventArgs e) 
        {  
            OnRespawning?.Invoke(e);
            Log.Debug($"{nameof(InvokeRespawning)} called");
        }
        internal static void InvokeRespawned(RespawnedEventArgs e) 
        { 
            OnRespawned?.Invoke(e);
            Log.Debug($"{nameof(InvokeRespawned)} called");
        }
    }
}
