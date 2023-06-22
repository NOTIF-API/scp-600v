using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extension;
using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp106
    {
        internal void OnPocketDemensionCapture(EvArg.Player.EnteringPocketDimensionEventArgs e)
        {
            if (CustomRole.Get(typeof(Scp600CotumRoleBase)).Check(e.Player) & e.Player != null)
            {
                Log.Debug("Scp-106 don't touch scp600 in pocket demension");
                e.IsAllowed = false;
            }
        }
    }
}