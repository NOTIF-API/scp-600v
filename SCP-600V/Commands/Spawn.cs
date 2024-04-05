using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;
using SCP_600V.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Spawn : ICommand
    {
        public string Command { get; set; } = "spawn";

        public string[] Aliases { get; set; } = { "s6cr" };

        public string Description { get; set; } = "allow spaawn your or other player as scp600";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.spawn"))
            {
                response = "do not have permissions";
                return false;
            }
            else
            {
                Player ply = null;
                if (arguments.Count > 0)
                {
                    ply = Player.Get(arguments.At(0));
                }
                if (arguments.Count == 0)
                {
                    ply = Player.Get(sender);
                }
                CustomRole.Get(typeof(Scp600)).AddRole(ply);
                response = $"succes added for {ply.Nickname}";
                return true;
            }
        }
    }
}
