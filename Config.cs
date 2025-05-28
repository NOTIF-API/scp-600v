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
        [Description("List of roles from which Scp 600 will be selected")]
        public List<RoleTypeId> SelectableRoles { get; set; } = new List<RoleTypeId>()
        {
            RoleTypeId.ClassD
        };
        [Description("The time between which the player can change his appearance")]
        public int AbilityCoolDown { get; set; } = 60;
        [Description("The message the player will see if they use their ability.")]
        public string AbilityUseMessage { get; set; } = "You have successfully changed your appearance with your ability to %role%";
        [Description("The message the player will see when refusing to use an ability (only if he is an object)")]
        public string AbilityUseDenyMessage { get; set; } = "You cannot use the ability until %second% seconds have passed.";
        [Description("Description of the top in user settings")]
        public string HeaderDescription { get; set; } = "Managing SCP-600's capabilities";
        [Description("Button bind parameter name")]
        public string KeyBindName { get; set; } = "Ability to change appearance (bind)";
        [Description("Description for the button bind parameter")]
        public string KeyBindDescription { get; set; } = "The key responsible for activating the ability to change appearance to random role";
        [Description("Button identifier for the bind, this value must be unique for its instance")]
        public int KeyBindId { get; set; } = 601;

        [Description("Scp 600 role config")]
        public Scp600v ScpRole { get; set; } = new();
    }
}
