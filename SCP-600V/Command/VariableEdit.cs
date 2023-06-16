using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class VariableEdit : ICommand
    {
        public string Command { get; set; } = "varedit";

        public string[] Aliases { get; set; } = { "ve" };

        public string Description { get; set; } = "varedit <add/remove> <tag> <userID>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            Player arguser = Player.Get(arguments.At(2));
            if (ply.CheckPermission("s6.debug"))
            {
                if (arguser != null)
                {
                    try
                    {
                        if (arguments.At(0) == "add")
                        {
                            if (!arguser.SessionVariables.ContainsKey(arguments.At(1)))
                            {
                                arguser.SessionVariables.Add(arguments.At(1), null);
                                response = "";
                                return true;
                            }
                            response = "IsContained";
                            return false;
                        }
                        if (arguments.At(0) == "remove")
                        {
                            if (!arguser.SessionVariables.ContainsKey(arguments.At(1)))
                            {
                                arguser.SessionVariables.Remove(arguments.At(1));
                                response = "";
                                return true;
                            }
                            response = "Not found";
                            return false;
                        }
                    }
                    catch
                    {
                        response = Sai.Instance.Config.PlayerNF;
                        return false;
                    }
                }
                if (arguser == null)
                {
                    if (arguments.At(0) == "add")
                    {
                        if (ply.SessionVariables.ContainsKey(arguments.At(1)))
                        {
                            ply.SessionVariables.Add(arguments.At(1), null);
                            response = "";
                            return true;
                        }
                        response = "IsContained";
                        return false;
                    }
                    if (arguments.At(0) == "remove")
                    {
                        if (ply.SessionVariables.ContainsKey(arguments.At(1)))
                        {
                            ply.SessionVariables.Remove(arguments.At(1));
                            response = "";
                            return true;
                        }
                        response = "Not found";
                        return false;
                    }
                }
            }
            if (ply.CheckPermission("s6.debug") == false)
            {
                response = Sai.Instance.Config.PermissionDenied;
                return false;
            }
            response = "idk";
            return false;
        }
    }
}
