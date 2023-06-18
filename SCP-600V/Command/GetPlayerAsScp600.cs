using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using System.Text;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class GetPlayerAsScp600 : ICommand
    {
        public string Command { get; set; } = "ListScp600Player";

        public string[] Aliases { get; set; } = { "LSP6", "li6" };

        public string Description { get; set; } = "Get player list playing as scp-600v now";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (player.CheckPermission("s6.GetPlayers"))
                {
                    StringBuilder a = new StringBuilder();
                    foreach (Player ply in Player.List)
                    {
                        if (ply.SessionVariables.ContainsKey("IsSCP600"))
                        {
                            a.Append(ply.Nickname + ", ");
                        }
                    }
                    response = $"{Sai.Instance.Config.ListGetted.Replace("{name}", a.ToString())}";
                    return true;
                }
                else
                {
                    response = Sai.Instance.Config.PermissionDenied;
                    return false;
                }
            }
            response = "? i have not get info";
            return false;
        }
    }
}