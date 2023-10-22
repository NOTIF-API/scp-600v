using Exiled.API.Features;
using SCP_600V.Events.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events
{
    public class Scp600Handler
    {
        public delegate void Spawning(SpawningEventArgs e);

        public delegate void Spawned(SpawnedEventArgs e);

        public static event Spawning OnSpawning;

        public static event Spawned OnSpawned;

        internal void InvokeSpawning(SpawningEventArgs e)
        {
            if (OnSpawning != null)
            {
                OnSpawning.Invoke(e);
            }
            else
            {
                Log.Debug("No have subscriders to event OnSpawning");
            }
        }
        internal void InvokeSpawned(SpawnedEventArgs e)
        {
            if (OnSpawned != null)
            {
                OnSpawned.Invoke(e);
            }
            else
            {
                Log.Debug("No have subscriders to event OnSpawned");
            }
        }
    }
}
