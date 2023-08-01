using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using HarmonyLib;
using SCP_600V.EventHandler;
using Handler = Exiled.Events.Handlers;

namespace SCP_600V
{
    internal class Sai: Plugin<Config>
    {
        
        public static Sai Instance;
        public Harmony _harma;
        public GameEvents _gameEvents;
        public RoundEvents _roundEvents;

        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            Log.Debug("creating an instance of harmony");
            _harma = new Harmony("com.scp600.Harmoni");
            _harma.PatchAll();
            this.Config.Scp600ConfigRole.Register();
            Log.Debug("Registered role scp600v in game");
            _gameEvents = new GameEvents();
            _roundEvents = new RoundEvents();
            //s1 = new EventHandler.GameEvent.Scp173();
            Handler.Server.RoundStarted += _roundEvents.OnRoundStarted;
            Handler.Server.EndingRound += _roundEvents.OnEndingRound;
            Handler.Player.EnteringPocketDimension += _gameEvents.EnterignPocketDemens;
            Handler.Player.Escaping += _gameEvents.OnEscape;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
            Instance = null;
            CustomRole.UnregisterRoles();
            _harma.UnpatchAll("com.scp600.Harmoni");
            _harma = null;
            _roundEvents = null;
            _gameEvents = null;

            Handler.Server.RoundStarted -= _roundEvents.OnRoundStarted;
            Handler.Server.EndingRound -= _roundEvents.OnEndingRound;
            Handler.Player.EnteringPocketDimension -= _gameEvents.EnterignPocketDemens;
            Handler.Player.Escaping -= _gameEvents.OnEscape;
        }
    }
}
