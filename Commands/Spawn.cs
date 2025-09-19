using System;

using CommandSystem;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.Permissions.Extensions;

using SCP_600V.Roles;

namespace SCP_600V.Commands
{
    public class Spawn : ICommand
    {
        public string Command { get; set; } = "spawn";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Spawn any player as SCP-600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.spawn"))
            {
                response = "<color=red>You do not have permission to use this command</color> <color=orange>(required have s6.spawn)</color>";
                return true;
            }
            Player target = arguments.Count > 0 ? Player.Get(arguments.At(0)) : Player.Get(sender);
            if (target.HasAnyCustomRole())
            {
                response = $"<color=yellow>{target.DisplayNickname} already has another custom role on the server</color>";
                return true;
            }
            Scp600v.RegisteredInstance?.AddRole(target);
            response = $"<color=green>The SCP-600 role was successfully assigned to the player</color>";
            return true;
        }
    }
}
