using System;
using System.Collections.Generic;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Commands
{
    public class svr : ICommand
    {
        public string Command { get; set; } = "svr";

        public string[] Aliases { get; set; } = new string[1] { "sessionvar" };

        public string Description { get; set; } = "Debug command for read session variables and values as string";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.debug"))
            {
                response = "You don't have permission for use this command (required have s6.debug)";
                return false;
            }

            Player target;
            if (arguments.Count == 0)
            {
                target = Player.Get(sender);
            }
            else
            {
                target = Player.Get(arguments.At(0));
            }

            if (target == null || target.IsHost)
            {
                response = "Player not found or the target can't readable object.";
                return false;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"--- Session Variables for {target.DisplayNickname} ---");

            if (target.SessionVariables.Count == 0)
            {
                stringBuilder.AppendLine("No session variables found.");
            }
            else
            {
                stringBuilder.Append("<color=green>");
                foreach (KeyValuePair<string, object> item in target.SessionVariables)
                {
                    try
                    {
                        string value = item.Value?.ToString() ?? "Null";
                        stringBuilder.AppendLine($"- {item.Key}: {value}");
                    }
                    catch (Exception ex)
                    {
                        Log.Debug($"Error retrieving session variable: {ex.Message}");
                        stringBuilder.AppendLine($"! {item.Key}: <Error retrieving value>");
                    }
                }
                stringBuilder.Append("</color>");
            }

            stringBuilder.AppendLine(new string('-', 20));
            response = stringBuilder.ToString();
            return true;
        }
    }
}
