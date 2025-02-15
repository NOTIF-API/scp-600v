using System;
using System.Linq;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;

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
            if (!sender.CheckPermission("s6.debug"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            else
            {
                CustomRole scp = CustomRole.Get(typeof(Scp600v));
                if (scp == null)
                {
                    response = "Could not find the role of Scp 600 as a registered object, perhaps it could not be registered";
                    return false;
                }
                Player[] a = Player.List.Where(x => scp.Check(x)).ToArray();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Active players:");
                if (a.Length == 0)
                {
                    stringBuilder.AppendLine("0 players");
                    response = stringBuilder.ToString();
                    return true;
                }
                foreach (Player item in a)
                {
                    stringBuilder.AppendLine($"{item.DisplayNickname}: {item.Health}/{item.MaxHealth} HP {item.ArtificialHealth}/{item.MaxArtificialHealth} AHP");
                }
                response = stringBuilder.ToString();
                return true;
            }
        }
    }
}
