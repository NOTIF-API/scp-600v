using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;
using Exiled.API.Features;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp173trigered
    {
        internal void Scp173Trigered(EvArg.Scp173.BlinkingEventArgs e)
        {
            int PlayerScp600 = 0;
            foreach (Player a in e.Targets)
            {
                if (api.IsScp600(a))
                {
                    PlayerScp600 += 1;
                }
            }
            if (PlayerScp600 == e.Targets.Count)
            {
                e.BlinkCooldown = 0;
                e.IsAllowed = false;
            }
        }
    }
}
