using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using HarmonyLib;
using System;

namespace SCP_600V
{
    internal class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(8, 6, 0);

        public override Version Version { get; } = new Version(3, 0, 0);

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }

        private Harmony Harmony;

        public override void OnEnabled()
        {
            Log.Debug("Creating instance of main class");
            Instance = this;
            Log.Debug("Creating harmony");
            Harmony = new Harmony("com.scp600.object");
            Log.Debug("Patching all in harmony");
            Harmony.PatchAll();
            Log.Debug("Registaring role scp600");
            this.Config.ScpConfig.Register();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Debug("Unregister role scp600");
            this.Config.ScpConfig.Unregister();
            Log.Debug("Unpatching harmony");
            Harmony.UnpatchAll();
            Instance = null;
            Harmony = null;
            base.OnDisabled();
        }

        private void RegisterEvent()
        {
            Log.Debug("Subscride to events");
        }

        private void UnRegisterEvent()
        {
            Log.Debug("Unsubscride to events");
        }
    }
}
