using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using SCP_600V.API.Role;

namespace SCP_600V.Command
{
    internal class Spawn : ICommand
    {
        public string Command { get; set; } = "spawn";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Spawn your as scp-600v\nsp6 <userID>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("s6.Spawn"))
            {
                response = "Your don't have permission's for use this command";
                return false;
            }
            Player GiveTo;
            if (arguments.Count > 0)
            {
                GiveTo = Player.Get(arguments.At(0));
                if (GiveTo == null)
                {
                    response = "Player not found by id";
                    return false;
                }
                Role.Spawn(GiveTo);
                response = $"Role successfully assigned to player {GiveTo.DisplayNickname}";
                return true;
            }
            else
            {
                GiveTo = Player.Get(sender);
                Role.Spawn(GiveTo);
                response = "role successfully issued (if nothing happened maybe you are an observer)";
                return true;
            }
        }
    }
}
