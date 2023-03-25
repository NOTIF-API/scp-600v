using PlayerRoles;
using api = SCP_600V.API.Players;
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
            if (ev.Player != null)
            {
                if (ev.Attacker != null)
                {
                    RoleTypeId role = ev.TargetOldRole;

                    List<Item.Item> items = new List<Item.Item>();

                    float mhea = ev.Attacker.MaxHealth;

                    float hea = ev.Attacker.Health;

                    Vector3 poso = ev.Attacker.Position;
                    Log.Debug($"Get all parameters");

                    if (ev.Attacker.Nickname != ev.Player.Nickname)
                    {
                        if (api.Scp600PlyGet.IsScp600(ev.Attacker))
                        {
                            Log.Debug("Attacker is scp600");
                            if (role != RoleTypeId.Spectator)
                            {
                                foreach (Item.Item a in ev.Attacker.Items)
                                {
                                    items.Add(a);
                                }
                                ev.Attacker.Role.Set(role, SpawnReason.ForceClass, RoleSpawnFlags.None);
                                ev.Attacker.Teleport(poso);
                                ev.Attacker.AddItem(items);
                                ev.Attacker.Health = hea;
                                ev.Attacker.MaxHealth = mhea;
                                ev.Attacker.Broadcast(message: $"{Sai.Instance.Config.MessageScpTransform.Replace("{player}", ev.Player.Nickname)}", duration: 5);
                                ev.Attacker.EnableEffect(new Effect(EffectType.Bleeding, 9999f));
                                ev.Attacker.Heal(10);
                                Log.Debug("Scp600 get new role");
                            }
                        }
                        if (api.Scp600PlyGet.IsScp600(ev.Player))
                        {
                            ev.Player.SessionVariables.Remove("IsSCP600");
                            ev.Player.SessionVariables.Remove("IsScp");
                            ev.Player.MaxHealth = 100;

                            ev.Player.CustomInfo = string.Empty;
                            ev.Player.InfoArea |= ~PlayerInfoArea.Nickname;
                            ev.Player.InfoArea |= ~PlayerInfoArea.UnitName;
                            Log.Debug("Remove all session variables and set default hp");
                            Log.Debug("scp600 player is dead");
                        }
                    }
                }
            }
        }
    }
}