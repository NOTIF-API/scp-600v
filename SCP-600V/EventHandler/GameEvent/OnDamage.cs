using System;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;
using PlayerRoles;
using Exiled.API.Features.Roles;

namespace SCP_600V.EventHandler.GameEvent
{
    internal class OnDamage
    {
        internal void DamageHandler(EvArg.Player.HurtingEventArgs e)
        {
            if ( e.Attacker != null )
            {
                if ( e.Player != null )
                {
                    if (api.IsScp600(e.Attacker) == true)
                    {
                        bool a = Scp600DamageHandler(e.Attacker, e.Player);
                        if ( a )
                        {
                            e.DamageHandler.Damage = e.DamageHandler.Damage * Sai.Instance.Config.MultipleDamage;
                            e.IsAllowed = true;
                        }
                        if ( a == false)
                        {
                            e.DamageHandler.Damage = 0;
                            e.IsAllowed = false;
                        }
                    }
                    if (api.IsScp600(e.Attacker) == false)
                    {
                        bool a = NoScp600DamageHandler(e.Attacker, e.Player);
                        if ( a )
                        {
                            e.IsAllowed = true;
                        }
                        if ( a == false )
                        {
                            e.DamageHandler.Damage = 0;
                            e.IsAllowed = false;
                        }
                    }
                }
            }
        }
        internal bool Scp600DamageHandler(Player atacker, Player player)
        {
            if (Sai.Instance.Config.IsScpCanDamageMe == false)
            {
                if (player.Role.Team == Team.SCPs)
                {
                    return false;
                }
                if (api.IsSH(player) || api.IsCustomScp(player) || api.IsScp035(player) || api.IsScp600(player))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
               
        internal bool NoScp600DamageHandler(Player atacker, Player player)
        {
            if (Sai.Instance.Config.IsFFEnabled == false)
            {
                if (Sai.Instance.Config.IsScpCanDamageMe == false)
                {
                    if (api.IsCustomScp(atacker) || api.IsSH(atacker) || api.IsScp035(atacker) || api.IsScp600(atacker) || atacker.Role.Team == Team.SCPs)
                    {
                        if (api.IsScp600(player))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (api.IsCustomScp(atacker) || api.IsSH(atacker) || api.IsScp035(atacker) || api.IsScp600(atacker) || atacker.Role.Team == Team.SCPs)
                {
                    if (api.IsScp600(player))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                if (atacker.Role.Team == player.Role.Team)
                {
                    if (api.IsScp600(player))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
