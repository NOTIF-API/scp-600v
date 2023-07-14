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

        public string Description { get; set; } = "Spawn your as scp-600v";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null & player.CheckPermission("s6.SelfSpawn") & !player.IsDead)
            {
                RoleSet.Spawn(player);
                response = "Done";
                return true;
            }
            else
            {
                response = "Your don't have permission or your are spectator";
                return false;
            }
        }
    }
}
