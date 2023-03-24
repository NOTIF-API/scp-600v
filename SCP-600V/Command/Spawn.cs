using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using FMOD;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Spawn : ICommand
    {
        public string Command { get; set; } = "SpawnScp600";

        public string[] Aliases { get; set; } = { "S600", "sp6" };

        public string Description { get; set; } = "Spawn your as scp-600v";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player writer = Player.Get(sender);
            if (writer != null)
            {
                if (writer.CheckPermission("s6.SelfSpawn"))
                {
                    if (writer.Role.Type != PlayerRoles.RoleTypeId.Spectator)
                    {
                        new Extension.Scp600(writer);
                        response = Sai.Instance.Config.SpawnMessage;
                        return true;
                    }
                    if (writer.Role.Type == PlayerRoles.RoleTypeId.Spectator)
                    {
                        response = Sai.Instance.Config.SpawnCommandEr;
                        return false;
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
