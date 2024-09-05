using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using System;

namespace SCP_600V
{
    internal class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(8, 9, 0);

        public override Version Version { get; } = new Version(3, 1, 0);

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            Log.Debug($"{nameof(OnEnabled)} Registaring role scp600");
            this.Config.ScpConfig.Register();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Debug($"{nameof(OnDisabled)} Unregister role scp600");
            this.Config.ScpConfig.Unregister();
            Instance = null;
            base.OnDisabled();
        }
        //at the moment I have no idea what to add here

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
