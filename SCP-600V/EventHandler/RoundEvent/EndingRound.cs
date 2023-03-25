using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;

namespace SCP_600V.EventHandler.RoundEvent
{
    internal class EndingRound
    {
        internal void OnEndingRound(EvArg.Server.EndingRoundEventArgs e)
        {
            if (api.GetScp600().Count != 0 | api.GetSH().Count != 0 | api.GetCostumSCP().Count != 0 | API.Players.Scp600PlyGet.GetScp035().Count != 0)
            {
                e.IsRoundEnded = false;
            }
        }
    }
}
