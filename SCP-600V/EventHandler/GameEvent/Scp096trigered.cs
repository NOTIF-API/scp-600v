using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;
using Exiled.API.Features;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp096trigered
    {
        internal void Scp096Trigered(EvArg.Scp096.ChargingEventArgs e)
        {
            if (api.IsScp600(e.Player))
            {
                e.IsAllowed = false;
            }
        }
    }
}
