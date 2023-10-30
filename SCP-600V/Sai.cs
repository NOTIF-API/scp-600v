using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using HarmonyLib;
using SCP_600V.EventHandler;
using UnityEngine;
using Handler = Exiled.Events.Handlers;

namespace SCP_600V
{
    internal class Sai: Plugin<Config>
    {
        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Sai Instance { get; private set; }
        /// <summary>
        /// The harmony class that is needed for the plugin to work correctly
        /// </summary>
        private Harmony _harma;
        /// <summary>
        /// The class where game event handlers are located
        /// </summary>
        private GameEvents _gameEvents;
        /// <summary>
        /// The class where game event handlers regarding rounds are located (end, start)
        /// </summary>
        private RoundEvents _roundEvents;
        /// <summary>
        /// Called when the plugin starts
        /// </summary>
        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;

            Log.Debug("creating an instance of harmony");

            _harma = new Harmony("com.scp600.Harmoni");
            _harma.PatchAll();
            this.Config.Scp600ConfigRole.Register();
            Log.Debug("Registered role scp600v in game");

            this.RegisterEvent();
        }
        /// <summary>
        /// Called when the plugin is disabled
        /// </summary>
        public override void OnDisabled()
        {
            base.OnDisabled();
            Instance = null;

            CustomRole.UnregisterRoles();

            _harma.UnpatchAll("com.scp600.Harmoni");
            _harma = null;

            this.UnRegisterEvent();
        }
        /// <summary>
        /// Called to register for an event
        /// </summary>
        private void RegisterEvent()
        {
            _gameEvents = new GameEvents();
            _roundEvents = new RoundEvents();

            Handler.Server.RoundStarted += _roundEvents.OnRoundStarted;
            Handler.Player.EnteringPocketDimension += _gameEvents.EnterignPocketDemens;
            Handler.Player.Hurting += _gameEvents.HurtingPlayer;
        }
        /// <summary>
        /// Called if you need to unsubscribe from all events
        /// </summary>
        private void UnRegisterEvent()
        {
            _roundEvents = null;
            _gameEvents = null;

            Handler.Server.RoundStarted -= _roundEvents.OnRoundStarted;
            Handler.Player.EnteringPocketDimension -= _gameEvents.EnterignPocketDemens;
            Handler.Player.Hurting -= _gameEvents.HurtingPlayer;
        }
    }
}
