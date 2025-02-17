using System;
using System.Text;

using CommandSystem;

using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class svr : ICommand
    {
        public string Command { get; set; } = "svr";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Debug command for read session variables and values as string";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.debug"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            else
            {
                Player target = arguments.Count == 0 ? Player.Get(sender) : Player.Get(arguments.At(0));
                if (target == null | target.IsHost)
                {
                    response = "the target does not exist or you are a console";
                    return false;
                }
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SessiongVariables");
                foreach (var item in target.SessionVariables)
                {
                    try
                    {
                        stringBuilder.AppendLine($"{item.Key}: {item.Value.ToString()}");
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex.Message);
                    }
                }
                response = stringBuilder.ToString();
                return true;
            }
        }
    }
}
