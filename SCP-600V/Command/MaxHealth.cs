﻿using System;
using Exiled.API.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using FMOD;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class MaxHealth : ICommand
    {
        public string Command { get; set; } = "MHP";

        public string[] Aliases { get; set; } = { "mhp", "s6mhp" };

        public string Description { get; set; } = "Chenge your maxhealt values";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (ply != null) 
            {
                if (ply.CheckPermission("s6.ChengeMHP"))
                {
                    if (ply.SessionVariables.ContainsKey("IsSCP600"))
                    {
                        if (arguments.Count == 0)
                        {
                            response = Sai.Instance.Config.MhpHealArgEr;
                            return false;
                        }
                        else
                        {
                            try
                            {
                                if (arguments.Count == 1)
                                {
                                    int num = int.Parse(arguments.At(0));
                                    ply.MaxHealth = num;
                                    ply.Health = num;
                                    response = $"{Sai.Instance.Config.MhpChenged.Replace("{amount}", num.ToString())}";
                                    return true;
                                }
                                if (arguments.Count == 2)
                                {
                                    Player who = Player.Get(arguments.At(1));
                                    if (who != null)
                                    {
                                        int num = int.Parse(arguments.At(0));
                                        who.MaxHealth = num;
                                        who.Health = num;
                                        response = $"{Sai.Instance.Config.MhpChenged.Replace("{amount}", num.ToString())}";
                                        return true;
                                    }
                                    if (who == null)
                                    {
                                        response = Sai.Instance.Config.PlayerNF + "\n mhp <amount> <userID>";
                                        return false;
                                    }
                                }
                            }
                            catch
                            {
                                response = Sai.Instance.Config.MhpHealArgEr;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        response = Sai.Instance.Config.NotScp600;
                    }
                }
                else
                {
                    response = Sai.Instance.Config.PermissionDenied;
                    return false;
                }
            }
            response = "?";
            return false;
        }
    }
}