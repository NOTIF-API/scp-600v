using System;
using System.Linq;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.Permissions.Extensions;

using SCP_600V.Extensions;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Lists : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command { get; set; } = "slist";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Get all player played now as scp600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.list"))
            {
                response = "You do not have permission s6.list to use this command";
                return false;
            }
            Player[] players = Player.List.Where(x => x.IsScp600()).ToArray();
            if (players.Length == 0)
            {
                response = "There are no users with the role Scp600";
                return false;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"At the moment {players.Length} players have been detected");
            foreach (Player p in players)
            {
                sb.AppendLine($"{p.DisplayNickname}: {p.Health}/{p.MaxHealth}-HP {p.ArtificialHealth}/{p.MaxArtificialHealth}-AHP");
            }
            response = sb.ToString();
            return true;
        }
    }
}
