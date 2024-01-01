using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using HarmonyLib;
using SCP_600V.EventHandler;
using System;
using UnityEngine;
using Handler = Exiled.Events.Handlers;

namespace SCP_600V
{
    internal class Main: Plugin<Config>
    {
        public override string Author => "notifapi";

        public override string Name => "SCP-600V";

        public override bool IgnoreRequiredVersionCheck => true;

        public override Version RequiredExiledVersion => new Version(8, 5, 0);

        public override Version Version => new Version(2, 3, 5);

        public override PluginPriority Priority => PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }
        /// <summary>
        /// The harmony class that is needed for the plugin to work correctly
        /// </summary>
        private Harmony _harma;
        /// <summary>
        /// The class where game event handlers are located
        /// </summary>
        private GameEvents _gameEvents;
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

            CustomRole.RegisterRoles(false, null);

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

            Handler.Player.EnteringPocketDimension += _gameEvents.EnterignPocketDemens;
            Handler.Player.Hurting += _gameEvents.HurtingPlayer;
        }
        /// <summary>
        /// Called if you need to unsubscribe from all events
        /// </summary>
        private void UnRegisterEvent()
        {
            _gameEvents = null;

            Handler.Player.EnteringPocketDimension -= _gameEvents.EnterignPocketDemens;
            Handler.Player.Hurting -= _gameEvents.HurtingPlayer;
        }
    }
}
