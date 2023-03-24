using System.Collections.Generic;
using Exiled.API.Features;
using Item = Exiled.API.Features.Items;
using EvArg = Exiled.Events.EventArgs;
using UnityEngine;
using Exiled.API.Features.Roles;
using PlayerRoles;
using Mirror;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;

namespace SCP_600V.Extension
{
    internal class Scp600
    {
        public static int MaxHealt { get; set; } = 0;
        public static bool IsScpCanDamage { get; set; } = false;

        public Scp600(Player player, int maxhealt = 0)
        {
            if (maxhealt != 0)
            {
                MaxHealt = maxhealt;
            }
            else
            {
                MaxHealt = Sai.Instance.Config.Maxhealt;
            }
            // adding attributes
            player.SessionVariables.Add("IsSCP600", null);
            // determines that it is a script for other plugins
            player.SessionVariables.Add("IsScp", null);
            // player stat's
            player.MaxHealth = MaxHealt;
            player.Health = MaxHealt;
            player.CustomInfo = $"{player.Nickname}\nSCP-600V-{API.Players.Scp600PlyGet.GetScp600().Count}";
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            UserGroup a = new UserGroup();
            a.KickPower = 0;
            a.RequiredKickPower = 0;
            a.BadgeColor = $"{Sai.Instance.Config.BadgeColor}";
            a.BadgeText = "SCP-600V";
            if (player.Group == null)
            {
                player.Group = a;
            }
            player.AddItem(ItemType.KeycardScientist);
            player.AddAmmo(AmmoType.Nato9, 30);
            player.Broadcast(message: $"<color=\"red\">{Sai.Instance.Config.SpawnMessage}</color>", duration: 5);
        }
    }
}
