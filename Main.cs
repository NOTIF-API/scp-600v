using System;
using System.Collections.Generic;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs.Player;

using SCP_600V.Features;

namespace SCP_600V
{
    public class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override Version Version { get; } = new Version(3, 4, 0);

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = new Main();
            SSS.Init();
            Log.Debug($"{nameof(OnEnabled)}: Registaring role scp600");
            this.Config.ScpRole.Register();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Debug($"{nameof(OnDisabled)}: Unregister role scp600");
            this.Config.ScpRole.Unregister();
            Instance = null;
            base.OnDisabled();
        }
    }
}
