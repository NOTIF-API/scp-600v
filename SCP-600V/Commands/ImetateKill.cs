using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ImetateKill : ICommand
    {
        public string Command { get; } = "fakekill";

        public string[] Aliases { get; } = { "fkl" };

        public string Description { get; } = "Created for test, you can do not use it";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.debug") | arguments.Count > 2)
            {
                response = "do not have permissions or arguments lower 2";
                return false;
            }
            else
            {
                Player a = Player.Get(arguments.At(0)); // attacker
                Player b = Player.Get(arguments.At(1)); // target
                b.Hurt(a, b.Health+1, Exiled.API.Enums.DamageType.Custom, null); // for kill target
                response = "done";
                return true;
            }
        }
    }
}
