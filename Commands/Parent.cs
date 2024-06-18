using CommandSystem;
using Exiled.API.Features;
using System;

namespace SCP_600V.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Parent : ParentCommand
    {
        public Parent() => LoadGeneratedCommands();

        public override string Command { get; } = "scp600";

        public override string[] Aliases { get; } = { "sp6" };

        public override string Description { get; } = "Handle behaviour of a plugin scp600v";

        public override void LoadGeneratedCommands()
        {
            Log.Debug("Registering other commands");
            RegisterCommand(new Lists());
            RegisterCommand(new Spawn());
            RegisterCommand(new ImetateKill());
            
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "sp6 slist | spawn";
            return true;
        }
    }
}
