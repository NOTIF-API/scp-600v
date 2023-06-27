using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using SCP_600V.API.Role;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnEscape
    {
        internal void OnEscaped(EvArg.Player.EscapingEventArgs e)
        {
            if (e.Player != null && RoleGet.IsScp600(e.Player))
            {
                Log.Debug($"Canceling escape scp600 player: [{e.Player.Nickname}]");
                e.IsAllowed = false;
            }
        }
    }
}
