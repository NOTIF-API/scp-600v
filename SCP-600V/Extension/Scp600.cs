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
using MEC;

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
            // player stat's
            player.MaxHealth = MaxHealt;
            player.Health = MaxHealt;
            SCP_600V.API.Players.Scp600manager.Add(player);
            player.AddItem(ItemType.KeycardScientist);
            foreach (KeyValuePair<AmmoType, ushort> a in Sai.Instance.Config.StartAmmo)
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    player.AddAmmo(a.Key, a.Value);
                });
            }
            //player.Broadcast(message: $"<color=\"red\">{Sai.Instance.Config.SpawnMessage}</color>", duration: 5);
            player.ShowHint($"\n\n\n\n\n\n\n<align=\"left\"><color=#FF0000>{Sai.Instance.Config.SpawnMessage}</color>");
            Log.Debug("Create new scp600");
        }
    }
}
