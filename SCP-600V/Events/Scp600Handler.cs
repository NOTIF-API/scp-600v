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
            OnSpawning?.Invoke(e);
        }
        internal void InvokeSpawned(SpawnedEventArgs e)
        {
            OnSpawned?.Invoke(e);
        }
    }
}
