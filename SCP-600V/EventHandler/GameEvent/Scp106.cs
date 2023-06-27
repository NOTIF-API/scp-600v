using Exiled.API.Features;
using SCP_600V.API.Role;
using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp106
    {
        internal void OnPocketDemensionCapture(EvArg.Player.EnteringPocketDimensionEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug("Scp-106 don't touch scp600 in pocket demension");
                e.IsAllowed = false;
            }
        }
    }
}