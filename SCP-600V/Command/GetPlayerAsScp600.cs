using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using System.Text;
using SCP_600V.API.Role;

namespace SCP_600V.Command
{
    internal class GetPlayerAsScp600 : ICommand
    {
        public string Command { get; set; } = "list";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Get player list playing as scp-600v now";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null && player.CheckPermission("s6.GetPlayers"))
            {
                StringBuilder playerListBuilder = new StringBuilder();
                foreach (Player ply in RoleGet.Scp600Players())
                {
                    playerListBuilder.Append(ply.Nickname).Append(", ");
                }
                string playerList = playerListBuilder.ToString().TrimEnd(',', ' '); // Удалить последнюю запятую и пробел
                response = $"{Sai.Instance.Config.ListGetted.Replace("{name}", playerList)}";
                return true;
            }
            else
            {
                response = "Your don't have permissons for use this command";
                return false;
            }
        }
    }
}