using System;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;

using SCP_600V.Roles;

namespace SCP_600V
{
    public class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(9, 9, 1);

        public override Version Version { get; } = new Version(3, 7, 0);

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = new Main();
            Log.Debug("[OnEnabled]: Registaring role scp600");
            this.Config.ScpRole.Register();
            Scp600v.RegisteredInstance = this.Config.ScpRole;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Debug($"[OnDisabled]: Unregister role scp600");
            this.Config.ScpRole.Unregister();
            Scp600v.RegisteredInstance = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}