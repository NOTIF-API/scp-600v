using System;

using CommandSystem;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;

using SCP_600V.Roles;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Spawn : ICommand
    {
        public string Command { get; set; } = "spawn";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Spawn your or specifiq player as scp600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission("s6.spawn"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            else
            {
                Player target = arguments.Count > 0 ? Player.Get(arguments.At(0)) : Player.Get(sender);
                if (target == null | target.IsHost)
                {
                    response = "the target does not exist or you are a console";
                    return false;
                }
                CustomRole.Get(typeof(Scp600v)).AddRole(target);
                response = "A role was successfully issued for player " + target.DisplayNickname;
                return true;
            }
        }
    }
}
