using PlayerRoles;
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
    internal class OnKilled
    {
        public void KillingPlayer(EvArg.Player.KillingPlayerEventArgs e)
        {
            if (e.Player != null& api.Scp600PlyGet.IsScp600(e.Player))
            {
                Log.Debug("Killing player argument");
                api.Scp600manager.Remove(e.Player);
            }
        }
    }
}
