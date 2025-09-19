using System;
using System.Linq;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.Permissions.Extensions;

using SCP_600V.API.Extensions;
using SCP_600V.Roles;

namespace SCP_600V.Commands
{
    public class List : ICommand
    {
        public string Command { get; set; } = "list";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Returns a list of players playing as SCP-600 and their stats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.list"))
            {
                response = "<color=red>You do not have permission to use this command</color> <color=orange>(required have s6.list)</color>";
                return true;
            }
            if (Scp600v.RegisteredInstance == null)
            {
                response = "<color=yellow>For unknown reasons, we were unable to find the role among the list of registered ones</color>";
                return true;
            }
            StringBuilder sb = new StringBuilder();
            Player[] catched = Player.List.Where(x => Scp600v.RegisteredInstance.Check(x)).ToArray();
            sb.AppendLine($"<color=green>Active players tracked:</color> <color=red>{catched.Length}</color>");
            sb.Append("<color=orange>");
            foreach (Player player in catched)
            {
                if (player.Health <= 0.1f) continue;
                sb.AppendLine($"{player.DisplayNickname}: [HP: {(int)player.Health}/{(int)player.MaxHealth} {(int)((player.Health/player.MaxHealth)*100)}%] AHP-{(int)player.ArtificialHealth}");
            }
            sb.Append("</color>");
            response = sb.ToString();
            return true;
        }
    }
}
