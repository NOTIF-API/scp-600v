using System;
using System.Collections.Generic;
using Exiled.API.Features;
using EvArg = Exiled.Events.EventArgs;
using api = SCP_600V.API.Players.Scp600PlyGet;

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
                    if (api.IsScp600(e.Player))
                    {
                        if (e.Attacker.Role.Team == PlayerRoles.Team.SCPs|| e.Attacker.SessionVariables.ContainsKey("IsScp")|| e.Attacker.SessionVariables.ContainsKey("IsScp035")|| e.Attacker.SessionVariables.ContainsKey("IsSH"))
                        {
                            if (!Sai.Instance.Config.IsScpCanDamageMe)
                            {
                                e.DamageHandler.Damage = 0;
                                e.IsAllowed = false;
                            }
                            else
                            {
                                e.IsAllowed = true;
                            }
                        }
                    }
                    if (api.IsScp600(e.Attacker))
                    {
                        if (e.Player.Role.Team == PlayerRoles.Team.SCPs)
                        {
                            e.DamageHandler.Damage = 0;
                            e.IsAllowed = false;
                        }
                    }
                }
            }
        }
    }
}
