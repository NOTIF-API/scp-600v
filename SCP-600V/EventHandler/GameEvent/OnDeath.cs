using PlayerRoles;
using SCP_600V.Command;
using System;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using Item = Exiled.API.Features.Items;
using UnityEngine;
using Exiled.API.Enums;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnDeath
    {
        public void OnPlayerKill(EvArg.Player.DiedEventArgs ev)
        {
            // роль умершего игрока до смерти
            RoleTypeId role = ev.TargetOldRole;
            //получаем убитого игрока
            Player deadplayer = ev.Player;
            // получаем убийцу
            Player killer = ev.Attacker;
            // инвентарь убийцы
            List<Item.Item> items = new List<Item.Item>();
            // максимум хп убийцы
            float mhea = killer.MaxHealth;
            // текущие хп убийцы
            float hea = killer.Health;
            // позиция убийцы
            Vector3 poso = killer.Position;
            //если убица наш сцп
            if (killer != deadplayer)
            {
                if (killer.SessionVariables.ContainsKey("IsSCP600"))
                {
                    if (role != RoleTypeId.Spectator)
                    {
                        foreach (Item.Item a in killer.Items)
                        {
                            items.Add(a);
                        }
                        killer.Role.Set(role, SpawnReason.ForceClass, RoleSpawnFlags.None);
                        killer.Teleport(poso);
                        killer.AddItem(items);
                        killer.Health = hea;
                        killer.MaxHealth = mhea;
                        killer.Broadcast(message: $"{Sai.Instance.Config.MessageScpTransform.Replace("{player}", deadplayer.Nickname)}", duration: 5);
                    }
                }
                if (deadplayer.SessionVariables.ContainsKey("IsSCP600"))
                {
                    deadplayer.SessionVariables.Remove("IsSCP600");
                    deadplayer.SessionVariables.Remove("IsScp");
                    //убираем тег групы
                    deadplayer.Group = null;
                    //возрощаем его хп на дефолт
                    deadplayer.MaxHealth = 100;
                    //убираем тег кастомного юнита
                    deadplayer.CustomInfo = $"{deadplayer.Nickname}";
                    deadplayer.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
                    deadplayer.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
                    deadplayer.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                }
            }
        }
    }
}