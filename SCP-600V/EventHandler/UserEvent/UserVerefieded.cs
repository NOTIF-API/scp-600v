using EvArg = Exiled.Events.EventArgs;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.Collections.Generic;

namespace SCP_600V.EventHandler.UserEvent
{
    internal class UserVerefieded
    {
        public List<string> PluginCreators { get; } = new List<string>() { "76561199170113302@steam" };
        public List<string> PluginTester { get; } = new List<string>() { "76561199029426406@steam", "76561199154285288@steam" };
        internal void OnVerefy(EvArg.Player.VerifiedEventArgs e)
        {
            if (Sai.Instance.Config.VisibleCreators)
            {
                if (PluginCreators.Contains(e.Player.UserId))
                {
                    if (e.Player.Group == null)
                    {
                        UserGroup group = new UserGroup();
                        group.BadgeColor = "yellow";
                        group.BadgeText = "Creatur plugin SCP-600V";
                        e.Player.Group = group;
                    }
                }
                if (PluginTester.Contains(e.Player.UserId))
                {
                    if (e.Player.Group == null)
                    {
                        UserGroup group = new UserGroup();
                        group.BadgeColor = "purple";
                        group.BadgeText = "Tester plugin SCP-600V";
                        e.Player.Group = group;
                    }
                }
            }
        }
    }
}
