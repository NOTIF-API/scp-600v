using System.ComponentModel;
using Exiled.API.Interfaces;
using SCP_600V.Roles;

namespace SCP_600V
{
    internal class Config : IConfig
    { 
        [Description("Enable or disable plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description("Can the player see the debug message in the server console")]
        public bool Debug { get; set; } = false;

        [Description("Configuration for the Scp600 role")]
        public Scp600 ScpConfig { get; set; } = new Scp600();
    }
}
