using System;
using System.Linq;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;

using SCP_600V.API.Extensions;
using SCP_600V.Roles;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class List : ICommand
    {
        public string Command { get; set; } = "list";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Returns a list of players playing as SCP-600 and their stats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.list"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            StringBuilder sb = new StringBuilder();
            Player[] catched = Player.List.Where(x => x.IsScp600()).ToArray();
            sb.AppendLine($"Active players tracked: {catched.Length}");
            foreach (Player player in catched)
            {
                sb.AppendLine($"{player.DisplayNickname}: HP-{player.Health} MHP-{player.MaxHealth}");
            }
            response = sb.ToString();
            return true;
        }
    }
}
