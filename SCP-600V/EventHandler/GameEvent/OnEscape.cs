using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnEscape
    {
        public void OnEscaped(EvArg.Player.EscapingEventArgs e)
        {
            if (e.Player != null)
            {
                if (api.IsScp600(e.Player))
                {
                    e.IsAllowed = false;
                }
            }
        }
    }
}
