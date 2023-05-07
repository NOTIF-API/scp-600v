﻿using System;
using System.Net.Http.Headers;
using Exiled.API.Features;
using Hand = Exiled.Events.Handlers;

namespace SCP_600V
{
    internal class Sai: Plugin<Config>
    {
        public static Sai Instance;
        public EventHandler.RoundEvent.StartingRound str;
        public EventHandler.RoundEvent.EndingRound er;
        public EventHandler.GameEvent.OnDamage odmg;
        public EventHandler.GameEvent.OnDeath ode;
        public EventHandler.GameEvent.OnRoleChenged orc;
        public EventHandler.GameEvent.Scp106 spd;
        public EventHandler.GameEvent.OnEscape osc;
        //public EventHandler.GameEvent.Scp173 s1;

        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            str = new EventHandler.RoundEvent.StartingRound();
            er = new EventHandler.RoundEvent.EndingRound();
            odmg = new EventHandler.GameEvent.OnDamage();
            ode = new EventHandler.GameEvent.OnDeath();
            orc = new EventHandler.GameEvent.OnRoleChenged();
            spd = new EventHandler.GameEvent.Scp106();
            osc = new SCP_600V.EventHandler.GameEvent.OnEscape();
            //s1 = new EventHandler.GameEvent.Scp173();
            Hand.Server.RoundStarted += str.OnRoundStarted;
            Hand.Server.EndingRound += er.OnEndingRound;
            Hand.Player.Hurting += odmg.DamageHandler;
            Hand.Player.Died += ode.OnPlayerKill;
            Hand.Player.ChangingRole += orc.OnRoleChenge;
            Hand.Player.EnteringPocketDimension += spd.OnPocketDemensionCapture;
            Hand.Player.Escaping += osc.OnEscaped;
            //Hand.Scp173.Blinking += s1.OnScp173Visibled;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
            Instance = null;
            str = null;
            er = null;
            odmg = null;
            ode = null;
            orc = null;
            spd = null;
            osc = null;
            //s1 = null;

            Hand.Server.RoundStarted -= str.OnRoundStarted;
            Hand.Server.EndingRound -= er.OnEndingRound;
            Hand.Player.Hurting -= odmg.DamageHandler;
            Hand.Player.Died -= ode.OnPlayerKill;
            Hand.Player.ChangingRole -= orc.OnRoleChenge;
            Hand.Player.EnteringPocketDimension -= spd.OnPocketDemensionCapture;
            Hand.Player.Escaping -= osc.OnEscaped;
            //Hand.Scp173.Blinking -= s1.OnScp173Visibled;
        }
    }
}
