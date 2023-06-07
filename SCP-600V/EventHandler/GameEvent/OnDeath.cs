﻿using PlayerRoles;
using api = SCP_600V.API.Players;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using Item = Exiled.API.Features.Items;
using UnityEngine;
using Exiled.API.Enums;
using MEC;
using Exiled.API.Features.Items;
using System;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnDeath
    {
        public void OnPlayerKill(EvArg.Player.DiedEventArgs ev)
        {
            if (ev.Player != null)
            {
                if (ev.Attacker != null)
                {
                    RoleTypeId role = ev.TargetOldRole;

                    List<Item.Item> items = new List<Item.Item>();

                    float mhea = ev.Attacker.MaxHealth;

                    float hea = ev.Attacker.Health;

                    Vector3 poso = ev.Attacker.Position;
                    Vector3 rota = ev.Attacker.Rotation;
                    Log.Debug($"Get all parameters");

                    if (ev.Attacker != ev.Player)
                    {
                        if (api.Scp600PlyGet.IsScp600(ev.Attacker))
                        {
                            Log.Debug("Attacker is scp600");
                            if (role != RoleTypeId.Spectator)
                            {
                                Dictionary<ItemType, ushort> amos = ev.Attacker.Ammo;
                                if (role != ev.Attacker.Role.Type)
                                {
                                    foreach (Item.Item a in ev.Attacker.Items)
                                    {
                                        if (!a.IsAmmo)
                                        {
                                            items.Add(a);
                                        }
                                    }
                                    Timing.CallDelayed(0.3f, () =>
                                    {
                                        Timing.CallDelayed(0.3f, () =>
                                        {
                                            foreach (KeyValuePair<ItemType, ushort> a in amos)
                                            {
                                                ev.Attacker.AddItem(a.Key, Convert.ToInt16(a.Value));
                                            }
                                        });
                                        ev.Attacker.Role.Set(role, SpawnReason.ForceClass, RoleSpawnFlags.None);
                                        ev.Attacker.Teleport(poso);
                                        ev.Attacker.Rotation = rota;
                                        ev.Attacker.AddItem(items);
                                        ev.Attacker.Health = hea;
                                        ev.Attacker.MaxHealth = mhea;
                                        //ev.Attacker.Broadcast(message: $"{Sai.Instance.Config.MessageScpTransform.Replace("{player}", ev.Player.Nickname)}", duration: 5);
                                        ev.Attacker.ShowHint($"\n\n\n\n\n\n\n<align=\"left\">{Sai.Instance.Config.MessageScpTransform.Replace("{player}", ev.Player.Nickname)}");
                                        ev.Player.ShowHint($"\n\n\n\n\n\n\n{Sai.Instance.Config.MessageDeathPlayerByScp600}");
                                        if (Sai.Instance.Config.CanBleading)
                                        {
                                            ev.Attacker.Heal(10);
                                        }
                                        Log.Debug("Scp600 get new role");
                                    });
                                }
                                else
                                {
                                    ev.Attacker.Heal(10);
                                }
                            }
                        }
                        if (api.Scp600PlyGet.IsScp600(ev.Player))
                        {
                            ev.Player.SessionVariables.Remove("IsSCP600");
                            ev.Player.SessionVariables.Remove("IsScp");
                            ev.Player.MaxHealth = 100;

                            //ev.Player.CustomInfo = string.Empty;
                            //ev.Player.InfoArea |= ~PlayerInfoArea.Nickname;
                            //ev.Player.InfoArea |= ~PlayerInfoArea.UnitName;
                            Log.Debug("Remove all session variables and set default hp");
                            Log.Debug("scp600 player is dead");
                        }
                        Timing.CallDelayed(1f, () =>
                        {
                            Extension.CheckNotIsLast.Start();
                        });
                    }
                }
            }
        }
    }
}