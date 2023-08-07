using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Command
{
    internal class PermissionList : ICommand
    {
        public string Command { get; set; } = "perms";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "displays a list of permissions for commands";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "s6.Spawn - can admins give scp-600 role other player and self\ns6.ChengeMHP - can admin change max health while playing as scp-600\ns6.debug - can admin get player's session variables\ns6.GetPlayers - can the admin find out the list of players playing for scp-600";
            return true;
        }
    }
}
