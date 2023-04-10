using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class Scp173
    {
        internal void OnScp173Visibled(EvArg.Scp173.BlinkingEventArgs e)
        {
            if (e.Player != null)
            {
                if (e.Targets != null)
                {
                    int Scp600ply = 0;
                    foreach (Player target in e.Targets) 
                    {
                        if (target != null)
                        {
                            if (api.IsScp600(target))
                            {
                                Scp600ply++;
                            }
                        }
                    }
                    if (e.Targets.Count == Scp600ply)
                    {
                        e.IsAllowed = false;
                    }
                    else
                    {
                        e.IsAllowed = true;
                    }
                }
            }
        }
    }
}