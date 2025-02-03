using System;

using CommandSystem;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;

using SCP_600V.Roles;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Spawn : ICommand
    {
        public bool SanitizeResponse => true;
        public string Command { get; set; } = "spawn";

        public string[] Aliases { get; set; } = { "s6cr" };

        public string Description { get; set; } = "allow spawn your or other player as scp600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.spawn"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }
            Player target = arguments.Count == 0 ? Player.Get(sender) : Player.Get(arguments.At(0));
            if (target == null)
            {
                response = "Target for getting role not found, error interacting with command";
                Log.Debug($"Spawn.{nameof(Execute)}: {arguments.Count} Command can be execute from console");
                return false;
            }
            try
            {
                CustomRole.Get(typeof(Scp600)).AddRole(target);
                response = "";
                return true;
            }
            catch (Exception ex)
            {
                Log.Debug($"Spawn.{nameof(Execute)}: {ex.Message}");
                response = "Error in command execution, details were recorded in Debug";
                return false;
            }
        }
    }
}
