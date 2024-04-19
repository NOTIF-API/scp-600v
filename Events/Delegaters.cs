using SCP_600V.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Events
{
    public delegate void Spawning(SpawningEventArg e);

    public delegate void Died(DiedEventArg e);
}
