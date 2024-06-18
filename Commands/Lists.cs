using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SCP_600V.Extensions;
using System;
using System.Linq;
using System.Text;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Lists : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command { get; set; } = "slist";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "get all player played now as scp600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.list")) { response = "do not have permissions"; return false; }
            else
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Player ply in Player.List.Where(x => x.IsAlive & x.IsScp600()).ToList())
                    {
                        sb.AppendLine($"{ply.Nickname}: {(int)ply.Health} Hp | {(int)ply.ArtificialHealth} Ahp");
                    }
                    response = sb.ToString();
                    return true;
                }
                catch
                {
                    Log.Debug("slist unkown error");
                    response = "command not complete detected error try again later";
                    return false;
                }
            }
        }
    }
}
