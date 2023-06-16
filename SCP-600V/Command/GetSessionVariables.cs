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
                Player send = Player.Get(sender);
                Player ply = Player.Get(arguments.At(0));
                if (send.CheckPermission("s6.debug"))
                {
                    if (send == null)
                    {
                        response = "Unkown error";
                        return false;
                    }
                    if (send != null)
                    {
                        StringBuilder asd = new StringBuilder();
                        if (ply != null)
                        {
                            foreach (string arga in ply.SessionVariables.Keys)
                            {
                                asd.Append(arga + ", ");
                            }
                        }
                        else
                        {
                            foreach (string arga in send.SessionVariables.Keys)
                            {
                                asd.Append(arga + ", ");
                            }
                        }
                        response = asd.ToString();
                        return true;
                    }
                    response = "idk";
                    return false;
                }
                else
                {
                    response = Sai.Instance.Config.PermissionDenied;
                    return false;
                }
            }
        }
    }
}