using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class PermissionList : ICommand
    {
        public string Command { get; set; } = "PermissionList";

        public string[] Aliases { get; set; } = { "permli", "permission" };

        public string Description { get; set; } = "displays a list of permissions for commands";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "s6.SelfSpawn - can an admin give himself the scp-600 role\ns6.ChengeMHP - can admin change max health while playing as scp-600\ns6.debug - can admin get player's session variables\ns6.GetPlayers - can the admin find out the list of players playing for scp-600";
            return true;
        }
    }
}
