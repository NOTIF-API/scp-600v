using SCP_600V.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events.Handlers
{
    public class Scp600Handler
    {
        /// <summary>
        /// called before spawn scp600
        /// </summary>
        public static event Spawning OnSpawning;
        /// <summary>
        /// called after death scp600
        /// </summary>
        public static event Died OnDied;

        internal void CallSpawning(SpawningEventArg e)
        {
            OnSpawning?.Invoke(e);
        }

        internal void CallDied(DiedEventArg e)
        {
            OnDied?.Invoke(e);
        }
    }
}
