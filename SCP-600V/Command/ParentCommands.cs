using CommandSystem;
using Exiled.CustomRoles.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ParentCommands : ParentCommand
    {
        public ParentCommands() => LoadGeneratedCommands();
        public override string Command { get; } = "SCP600";

        public override string[] Aliases { get; } = new string[] { "s6" };

        public override string Description { get; } = "Interacting with SCP-600V plugins and executing various commands";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new GetPlayerAsScp600());
            RegisterCommand(new GetSessionVariables());
            RegisterCommand(new MaxHealth());
            RegisterCommand(new PermissionList());
            RegisterCommand(new Spawn());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "s6 spawn | list | vars | mhp | perms";
            return false;
        }
    }
}
