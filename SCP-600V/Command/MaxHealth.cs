using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using SCP_600V.API.Role;

namespace SCP_600V.Command
{
    internal class MaxHealth : ICommand
    {
        public string Command { get; set; } = "mhp";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Chenge your maxhealt values\nmhp <amount>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (ply != null && ply.CheckPermission("s6.ChengeMHP") && Role.IsScp600(ply))
            {
                string arg = arguments.At(0);
                if (!string.IsNullOrEmpty(arg) & int.TryParse(arg, out _))
                {
                    ply.MaxHealth = float.Parse(arg);
                    ply.Health = float.Parse(arg);
                    response = $"our lives have been successfully changed to {arg}, just like your current health.";
                    return true;
                }
                else
                {
                    response = "You have specified the wrong format or an empty argument. The correct format is an integer number.";
                    return false;
                }
            }
            else
            {
                response = "You are not authorized to use this command or you are not an SCP-600V.";
                return false;
            }
        }
    }
}