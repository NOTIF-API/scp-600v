using System.Collections.Generic;
using System.ComponentModel;

using Exiled.API.Interfaces;

using PlayerRoles;

using SCP_600V.Roles;

namespace SCP_600V
{
    public class Config : IConfig
    { 
        [Description("Enable or disable plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description("Can the player see the debug message in the server console")]
        public bool Debug { get; set; } = false;
        [Description("The message the player will see if they use their ability.")]
        public string AbilityUseMessage { get; set; } = "You have successfully changed your appearance with your ability to %role%";

        [Description("Scp 600 role config")]
        public Scp600v ScpRole { get; set; } = new();
    }
}
