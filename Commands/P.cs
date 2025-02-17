using System;
using System.Linq;

using CommandSystem;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class P : ParentCommand
    {
        public P() => LoadGeneratedCommands();

        public override string Command { get; } = "sp6";

        public override string[] Aliases { get; } = new[] { "scp600", "s6" };

        public override string Description { get; } = "main command handler for scp600";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new List());
            RegisterCommand(new Spawn());
            RegisterCommand(new svr());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"sp6 {string.Join(" | ", Commands.Select(x => x.Key))}";
            return true;
        }
    }
}