using System;

using CommandSystem;

namespace SCP_600V.Commands
{
    public class P : ParentCommand
    {
        public P() => LoadGeneratedCommands();

        public override string Command { get; } = "sp6";

        public override string[] Aliases { get; } = new string[] { "scp600", "s6" };

        public override string Description { get; } = "main command handler for scp600";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new List());
            RegisterCommand(new Spawn());
            RegisterCommand(new svr());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "sp6 list | spawn | svr";
            return true;
        }
    }
}