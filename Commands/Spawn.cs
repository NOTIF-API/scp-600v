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

        public string Description { get; set; } = Main.Instance.Config.SpawnCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.spawn"))
            {
                response = Main.Instance.Config.UnpermitedMessage.Replace("%permission%", "s6.spawn");
                return true;
            }
            Player target = arguments.Count > 0 ? Player.Get(arguments.At(0)) : Player.Get(sender);
            if (target.HasAnyCustomRole())
            {
                response = Main.Instance.Config.SpawnCommandAlerdyHasRole;
                return true;
            }
            Scp600v.RegisteredInstance?.AddRole(target);
            response = Main.Instance.Config.SpawnCommandSuccess.Replace("%player%", target.DisplayNickname);
            return true;
        }
    }
}
