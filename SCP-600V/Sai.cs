using System;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using HarmonyLib;
using SCP_600V.Extension;
using Hand = Exiled.Events.Handlers;

namespace SCP_600V
{
    internal class Sai: Plugin<Config>
    {
        
        public static Sai Instance;
        public Harmony _harma;
        public EventHandler.RoundEvent.StartingRound str;
        public EventHandler.RoundEvent.EndingRound er;
        public EventHandler.GameEvent.OnRoleChenged orc;
        public EventHandler.GameEvent.Scp106 spd;
        public EventHandler.GameEvent.OnEscape osc;

        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            Log.Debug("creating an instance of harmony");
            _harma = new Harmony("com.scp600.Harmoni");
            _harma.PatchAll();
            this.Config.Scp600ConfigRole.Register();
            Log.Debug("Registered role scp600v in game");
            str = new EventHandler.RoundEvent.StartingRound();
            er = new EventHandler.RoundEvent.EndingRound();
            orc = new EventHandler.GameEvent.OnRoleChenged();
            spd = new EventHandler.GameEvent.Scp106();
            osc = new SCP_600V.EventHandler.GameEvent.OnEscape();
            //s1 = new EventHandler.GameEvent.Scp173();
            Hand.Server.RoundStarted += str.OnRoundStarted;
            Hand.Server.EndingRound += er.OnEndingRound;
            Hand.Player.ChangingRole += orc.OnRoleChenge;
            Hand.Player.EnteringPocketDimension += spd.OnPocketDemensionCapture;
            Hand.Player.Escaping += osc.OnEscaped;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
            Instance = null;
            CustomRole.UnregisterRoles();
            _harma.UnpatchAll("com.scp600.Harmoni");
            str = null;
            er = null;
            orc = null;
            spd = null;
            osc = null;

            Hand.Server.RoundStarted -= str.OnRoundStarted;
            Hand.Server.EndingRound -= er.OnEndingRound;
            Hand.Player.ChangingRole -= orc.OnRoleChenge;
            Hand.Player.EnteringPocketDimension -= spd.OnPocketDemensionCapture;
            Hand.Player.Escaping -= osc.OnEscaped;
        }
    }
}
