using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;

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
                Player ply = Player.Get(arguments.At(0));
                if (sender.CheckPermission("s6.debug"))
                {
                    if (ply == null)
                    {
                        response = Sai.Instance.Config.PlayerNF;
                        return false;
                    }
                    if (ply != null)
                    {
                        string asd = "";
                        foreach (string arga in ply.SessionVariables.Keys)
                        {
                            asd += arga + " ";
                        }
                        response = asd;
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