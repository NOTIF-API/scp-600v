using System;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;

namespace SCP_600V
{
    internal class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override Version Version { get; } = new Version(3, 2, 0);

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
    }
}
