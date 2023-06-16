using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extension;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnEscape
    {
        public void OnEscaped(EvArg.Player.EscapingEventArgs e)
        {
            if (e.Player != null)
            {
                if (CustomRole.Get(typeof(Scp600CotumRoleBase)).Check(e.Player))
                {
                    Log.Debug($"Canceling escape scp600 player: [{e.Player.Nickname}]");
                    e.IsAllowed = false;
                }
            }
        }
    }
}
