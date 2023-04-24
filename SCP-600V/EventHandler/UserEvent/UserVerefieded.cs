using EvArg = Exiled.Events.EventArgs;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.Collections.Generic;

namespace SCP_600V.EventHandler.UserEvent
{
    internal class UserVerefieded
    {
        // Default no visible in game :) my friend is bad idea give me
        public List<string> PluginCreators { get; } = new List<string>() { "76561199170113302@steam" };
        public List<string> PluginTester { get; } = new List<string>() { "76561199029426406@steam", "76561199154285288@steam" };
        internal void OnVerefy(EvArg.Player.VerifiedEventArgs e)
        {
            if (Sai.Instance.Config.VisibleCreators)
            {
                if (PluginCreators.Contains(e.Player.UserId))
                {

                    UserGroup group = new UserGroup();
                    group.BadgeColor = "yellow";
                    group.BadgeText = "creators of the SCP-600V plugin";
                    e.Player.Group = group;

                }
                if (PluginTester.Contains(e.Player.UserId))
                {
                    
                    UserGroup group = new UserGroup();
                    group.BadgeColor = "purple";
                    group.BadgeText = "SCP-600V plugin testers";
                    e.Player.Group = group;
                    
                }
            }
        }
    }
}
