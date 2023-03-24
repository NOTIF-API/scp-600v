using EvArg = Exiled.Events.EventArgs;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp106
    {
        public void OnPocketDemensionCapture(EvArg.Player.EnteringPocketDimensionEventArgs e)
        {
            if (e.Player.SessionVariables.ContainsKey("IsSCP600"))
            {
                if (!Sai.Instance.Config.IsScpCanDamageMe)
                {
                    e.IsAllowed = false;
                }
            }
        }
    }
}