using System;
using System.Collections.Generic;
using System.Linq;

using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;

using PlayerRoles;

using SCP_600V.API.Extensions;
using SCP_600V.Roles;

using UnityEngine;

namespace SCP_600V.Features
{
    public class SSS
    {
        public static int CollDown { get; private set; }

        private static KeybindSetting Bottun { get; set; }

        private static HeaderSetting Header { get; set; }

        private static Dictionary<Player, DateTime> HoldCD { get; set; } = new Dictionary<Player, DateTime>();

        public static RoleTypeId[] SelectableToRandom { get; private set; }

        public static void Init()
        {
            RoleTypeId[] msv = (RoleTypeId[])Enum.GetValues(typeof(RoleTypeId));
            SelectableToRandom = msv.Where(x => x.IsAlive() && !x.IsScp() && !x.IsFlamingo() && x != RoleTypeId.Tutorial && x != RoleTypeId.CustomRole).ToArray(); // so as not to select invisible roles and Scp we get a list by sorting (it will help if new roles also appear)
            Config cfg = Main.Instance.Config;
            Header = new HeaderSetting("SCP-600", cfg.HeaderDescription);
            Bottun = new KeybindSetting(cfg.KeyBindId, cfg.KeyBindName, UnityEngine.KeyCode.B, false, cfg.KeyBindDescription, Header, OnChange);
            CollDown = cfg.AbilityCoolDown;
            SettingBase.Register(new SettingBase[]{ Bottun });
        }
        
        private static void OnChange(Player ply, SettingBase bnd)
        {
            if (!ply.IsScp600()) return;
            KeybindSetting key = bnd as KeybindSetting;
            if (key == null) return;
            if (HoldCD.ContainsKey(ply))
            {
                TimeSpan cd = DateTime.Now - HoldCD[ply];
                if (cd.TotalSeconds > CollDown)
                {
                    UseSSAbility(ply);
                    return;
                }
                else
                {
                    if (key.IsPressed)
                    {
                        ply.ShowHint(Main.Instance.Config.AbilityUseDenyMessage.Replace("%second%", ((int)(CollDown-cd.TotalSeconds)).ToString()));
                    }
                    return;
                }
            }
            UseSSAbility(ply);
        }

        private static void UseSSAbility(Player ply)
        {
            RoleTypeId get = SelectableToRandom.RandomItem();
            Scp600v.ChangeApperance(ply, get);
            HoldCD[ply] = DateTime.Now;
            ply.ShowHint(Main.Instance.Config.AbilityUseMessage.Replace("%role%", get.ToString()), 5);
        }
    }
}
