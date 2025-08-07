using System;
using System.Collections.Generic;
using System.Linq;

using Exiled.API.Enums;
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
            SelectableToRandom = msv.Where(x => RoleSelectPredicate(x)).ToArray(); // so as not to select invisible roles and Scp we get a list by sorting (it will help if new roles also appear)
            Config cfg = Main.Instance.Config;
            Header = new HeaderSetting(cfg.HeaderId, "SCP-600", cfg.HeaderDescription, false);
            Bottun = new KeybindSetting(cfg.KeyBindId, cfg.KeyBindName, KeyCode.B, false, false, cfg.KeyBindDescription, 255, Header, OnChengeState);
            CollDown = cfg.AbilityCoolDown;
            SettingBase.Register(new SettingBase[]{ Bottun });
        }
        private static void OnChengeState(Player ply, SettingBase sbase)
        {
            if (!ply.IsScp600()) return;
            if (sbase is KeybindSetting key)
            {
                if (key.IsPressed)
                {
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
                            ply.ShowHint(Main.Instance.Config.AbilityUseDenyMessage.Replace("%second%", ((int)(CollDown - cd.TotalSeconds)).ToString()));
                        }
                    }
                }
            }
        }
        private static void UseSSAbility(Player ply)
        {
            RoleTypeId get = SelectableToRandom.RandomItem();
            Scp600v.ChangeApperance(ply, get);
            HoldCD[ply] = DateTime.Now;
            ply.ShowHint(Main.Instance.Config.AbilityUseMessage.Replace("%role%", get.ToString()), 5);
        }

        private static bool RoleSelectPredicate(RoleTypeId role)
        {
            Side side = role.GetSide();
            if (side == Side.Scp || side == Side.Tutorial || side == Side.None || side.ToString() == "Flamingo") return false;
            return true;
        }
    }
}
