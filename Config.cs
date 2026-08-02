using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Exiled.API.Interfaces;

using PlayerRoles;

using SCP_600V.Roles;
using SCP_600V.Serelizables;

namespace SCP_600V
{
    public class Config : IConfig
    { 
        [Description("Enable or disable plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description("Can the player see the debug message in the server console")]
        public bool Debug { get; set; } = false;

        [Description("The message the player will see if they use their ability.")]
        public ScreenMessageSerelizable AbilityUseMessage { get; set; } = new ScreenMessageSerelizable()
        {
            Message = "<color=green>You have changed your appearance to %role%</color>",
            Duration = 5,
            MessageType = Exiled.API.Enums.MessageType.Broadcast
        };
        [Description("Description for the list command.")]
        public string ListCommandDescription { get; set; } = "Returns a list of players playing as SCP-600 and their stats";
        [Description("Message displayed for tracked players.")]
        public string ListCommandTracked { get; set; } = "<color=green>Active players tracked:</color> <color=red>{count}</color>";
        [Description("Description for the spawn command.")]
        public string SpawnCommandDescription { get; set; } = "Spawns a player as SCP-600";
        [Description("Message displayed when a player already has a custom role.")]
        public string SpawnCommandAlerdyHasRole { get; set; } = "<color=red>The player already has a custom role</color>";
        [Description("Message displayed when a player is successfully spawned as SCP-600.")]
        public string SpawnCommandSuccess { get; set; } = "<color=green>Successfully spawned %player% as SCP-600</color>";
        [Description("Message displayed when a player does not have permission to use the command.")]
        public string UnpermitedMessage { get; set; } = "<color=red>You do not have permission to use this command</color> <color=orange>(required have %permission%)</color>";
        [Description("List of roles that the object can take as its appearance.")]
        public List<RoleTypeId> ApperancableRoles { get; set; } = ((RoleTypeId[])Enum.GetValues(typeof(RoleTypeId))).Where(x => x.IsHuman() && x != RoleTypeId.Tutorial && x != RoleTypeId.CustomRole && x != RoleTypeId.Flamingo && x != RoleTypeId.AlphaFlamingo && x != RoleTypeId.ZombieFlamingo).ToList();
        [Description("Scp 600 role config")]
        public Scp600v ScpRole { get; set; } = new();
    }
}
