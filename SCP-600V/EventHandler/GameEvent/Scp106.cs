using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extension;
using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp106
    {
        public void OnPocketDemensionCapture(EvArg.Player.EnteringPocketDimensionEventArgs e)
        {
            if (e.Player.SessionVariables.ContainsKey("IsSCP600"))
            {
                if (CustomRole.Get(typeof(Scp600CotumRoleBase)).Check(e.Player))
                {
                    Log.Debug("Scp-106 don't touch scp600 in pocket demension");
                    e.IsAllowed = false;
                }
            }
        }
    }
}