﻿using Exiled.API.Features;
using SCP_600V.API.Players;
using Exiled.API.Enums;

namespace SCP_600V.API.Players
{
    internal class Scp600manager
    {
        internal static void Remove(Player ply)
        {
            if(Scp600PlyGet.IsScp600(ply))
            {
                ply.SessionVariables.Remove("IsSCP600");
                ply.SessionVariables.Remove("IsScp");
                ply.MaxHealth = 100;

                ply.CustomInfo = string.Empty;
                ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
                ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
                ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                ply.InfoArea |= ~PlayerInfoArea.Nickname;
                ply.InfoArea |= ~PlayerInfoArea.UnitName;
                ply.DisableAllEffects();
                Log.Debug("Remove all session variables and set default hp");
            }
        }
        internal static void Add(Player ply)
        {
            ply.SessionVariables.Add("IsSCP600", null);
            ply.SessionVariables.Add("IsScp", null);
            UserGroup a = new UserGroup();
            a.KickPower = 0;
            a.RequiredKickPower = 0;
            a.BadgeColor = $"{Sai.Instance.Config.BadgeColor}";
            a.BadgeText = "SCP-600V";
            if (ply.Group == null)
            {
                ply.Group = a;
            }
            ply.CustomInfo = $"{ply.Nickname}\nSCP-600V-{Scp600PlyGet.GetScp600().Count}";
            ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
            ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
            ply.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            ply.EnableEffect(new Effect(EffectType.Bleeding, 9999f));
        }
    }
}