using System;
using System.Collections.Generic;
using CommandSystem;
using CommandSystem.Commands.Console;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using PluginAPI.Enums;
using UnityEngine;
using Item = Exiled.API.Features.Items;

namespace SCP_600V.Command
{
    [CommandHandler(typeof(CommandHandler))]
    internal class ChengeClass : ICommand
    {
        public string Command { get; set; } = "ChangeClass";

        public string[] Aliases { get; set; } = { "ChCls", "newclass" };

        public string Description { get; set; } = "changes class 1 attemp per game to classd, security, mtf, scientist";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (ply.SessionVariables.ContainsKey("IsSCP600") == false)
            {
                response = $"<color=\"red\">{Sai.Instance.Config.NotScp600}</color>";
                return false;
            }
            if (ply.SessionVariables.ContainsKey("FullAt"))
            {
                response = $"{Sai.Instance.Config.AtempLimitMessage}";
                return false;
            }
            if (ply.SessionVariables.ContainsKey("IsSCP600"))
            {
                float healt = ply.Health;
                float max = ply.MaxHealth;
                if (arguments.Count == 0)
                {
                    response = $"{Sai.Instance.Config.ClassType.Replace("{name}", "classd, security, mtf, scientist")}";
                    return false;
                }
                if (arguments.At(0) == "classd") 
                {
                    List<Item.Item> items = new List<Item.Item>();
                    Vector3 pos = ply.Position;
                    foreach (Item.Item a in ply.Items) { items.Add(a); }
                    ply.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                    ply.ClearInventory();
                    ply.AddItem(items);
                    ply.Teleport(pos);
                    ply.SessionVariables.Add("FullAt", null);
                    ply.MaxHealth = max;
                    ply.Health = healt;
                    response = $"{Sai.Instance.Config.CompleteTransform}";
                    return true;
                }
                if (arguments.At(0) == "security") 
                {
                    List<Item.Item> items = new List<Item.Item>();
                    Vector3 pos = ply.Position;
                    foreach (Item.Item a in ply.Items) { items.Add(a); }
                    ply.Role.Set(PlayerRoles.RoleTypeId.FacilityGuard);
                    ply.ClearInventory();
                    ply.AddItem(items);
                    ply.Teleport(pos);
                    ply.SessionVariables.Add("FullAt", null);
                    ply.MaxHealth = max;
                    ply.Health = healt;
                    response = $"{Sai.Instance.Config.CompleteTransform}";
                    return true;
                }
                if (arguments.At(0) == "mtf") 
                {
                    List<Item.Item> items = new List<Item.Item>();
                    Vector3 pos = ply.Position;
                    foreach (Item.Item a in ply.Items) { items.Add(a); }
                    ply.Role.Set(PlayerRoles.RoleTypeId.NtfSergeant);
                    ply.ClearInventory();
                    ply.AddItem(items);
                    ply.Teleport(pos);
                    ply.SessionVariables.Add("FullAt", null);
                    ply.MaxHealth = max;
                    ply.Health = healt;
                    response = $"{Sai.Instance.Config.CompleteTransform}";
                    return true;
                }
                if (arguments.At(0) == "scientist") 
                {
                    List<Item.Item> items = new List<Item.Item>();
                    Vector3 pos = ply.Position;
                    foreach (Item.Item a in ply.Items) { items.Add(a); }
                    ply.Role.Set(PlayerRoles.RoleTypeId.Scientist);
                    ply.ClearInventory();
                    ply.AddItem(items);
                    ply.Teleport(pos);
                    ply.SessionVariables.Add("FullAt", null);
                    ply.MaxHealth = max;
                    ply.Health = healt;
                    response = $"{Sai.Instance.Config.CompleteTransform}";
                    return true;
                }
            }
            response = "?";
            return false;
        }
    }
}
