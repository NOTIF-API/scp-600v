using CommandSystem;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ImetateKill : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command { get; } = "testkill";

        public string[] Aliases { get; } = { "tkill" };

        public string Description { get; } = "Created for test, you can do not use it";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.debug"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }
            Player player = Player.Get(arguments.At(0)); 
            Player attacker = Player.Get(arguments.At(1));
            if (player == null | attacker == null)
            {
                response = "Players with such ID or name may not be valid or do not exist";
                return false;
            }
            player.Hurt(attacker, player.MaxHealth + 1, DamageType.Custom, null, "SCP-600 Test kill");
            response = "Command done";
            return true;
        }
    }
}
