using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using System.Text;

namespace SCP_600V.Command
{
    internal class GetSessionVariables
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        internal class Spawn : ICommand
        {
            public string Command { get; set; } = "Vars";

            public string[] Aliases { get; set; } = { "vars", "servervars" };

            public string Description { get; set; } = "Get player sessions variables";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                Player player = Player.Get(sender);
                if (player != null && player.CheckPermission("s6.debug"))
                {
                    StringBuilder variableListBuilder = new StringBuilder();
                    foreach (string arga in player.SessionVariables.Keys)
                    {
                        variableListBuilder.Append(arga).Append(", ");
                    }
                    string variableList = variableListBuilder.ToString().TrimEnd(',', ' '); // Удалить последнюю запятую и пробел
                    response = variableList;
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
}