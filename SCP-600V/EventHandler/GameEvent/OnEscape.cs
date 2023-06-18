using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Extension;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnEscape
    {
        public void OnEscaped(EvArg.Player.EscapingEventArgs e)
        {
            if (CustomRole.Get(typeof(Scp600CotumRoleBase)).Check(e.Player)&e.Player != null)
            {
                Log.Debug($"Canceling escape scp600 player: [{e.Player.Nickname}]");
                e.IsAllowed = false;
            }
        }
    }
}
