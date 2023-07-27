using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using SCP_600V.API.Role;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Spawn : ICommand
    {
        public string Command { get; set; } = "SpawnScp600";

        public string[] Aliases { get; set; } = { "S600", "sp6", "scp600" };

        public string Description { get; set; } = "Spawn your as scp-600v\nsp6 <userID>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null && player.CheckPermission("s6.SelfSpawn") && !player.Role.IsDead)
            {
                RoleSet.Spawn(player);
                response = "You have successfully acquired the role of SCP-600V";
                return true;
            }
            else
            {
                response = "At the moment you have the role of an observer or you are dead";
                return false;
            }
        }
    }
}
