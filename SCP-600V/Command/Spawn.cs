using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Spawn : ICommand
    {
        public string Command { get; set; } = "SpawnScp600";

        public string[] Aliases { get; set; } = { "S600", "sp6", "scp600" };

        public string Description { get; set; } = "Spawn your as scp-600v\nsp6 <userID>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player writer = Player.Get(sender);
            if (writer != null)
            {
                if (writer.CheckPermission("s6.SelfSpawn"))
                {
                    if (arguments.Count == 0)
                    {
                        if (writer.Role.Type != PlayerRoles.RoleTypeId.Spectator)
                        {
                            if (!API.Players.Scp600PlyGet.IsScp600(writer))
                            {
                                new Extension.Scp600(writer);
                                response = Sai.Instance.Config.SpawnMessage;
                                return true;
                            }
                            else
                            {
                                response = "? your are scp-600 now";
                                return false;
                            }
                        }
                        if (writer.Role.Type == PlayerRoles.RoleTypeId.Spectator)
                        {
                            response = Sai.Instance.Config.SpawnCommandEr;
                            return false;
                        }
                    }
                    else
                    {
                        Player neded = Player.Get(arguments.At(0));
                        if (neded != null)
                        {
                            if (neded.Role.Type == PlayerRoles.RoleTypeId.Spectator)
                            {
                                response = Sai.Instance.Config.SpawnCommandEr;
                                return false;
                            }
                            if (!API.Players.Scp600PlyGet.IsScp600(neded))
                            {
                                new Extension.Scp600(neded);
                                response = Sai.Instance.Config.SpawnMessage;
                                return true;
                            }
                            else
                            {
                                response = "? is scp-600 playing now";
                                return false;
                            }
                        }
                        if (neded == null)
                        {
                            response = Sai.Instance.Config.PlayerNF;
                            return false;
                        }
                    }
                }
                else
                {
                    response = Sai.Instance.Config.PermissionDenied;
                    return false;
                }
            }
            response = "Idk";
            return false;
        }
    }
}
