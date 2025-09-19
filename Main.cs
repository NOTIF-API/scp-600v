using System;
using System.Linq;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;

using PlayerRoles;

using SCP_600V.Roles;

namespace SCP_600V
{
    public class Main: Plugin<Config>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "scp600v";

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public override Version RequiredExiledVersion { get; } = new Version(9, 4, 0);

        public override Version Version { get; } = new Version(3, 6, 0);

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        /// <summary>
        /// A static variable called Instance is created to gain access to the plugin configurations and other parameters
        /// </summary>
        public static Main Instance { get; private set; }
        /// <summary>
        /// List of roles that the object can take as its appearance.
        /// </summary>
        public static RoleTypeId[] ApperaceableRoles { get; private set; } = new RoleTypeId[0];

        public override void OnEnabled()
        {
            Instance = new Main();
            Log.Debug($"{nameof(OnEnabled)}: Registaring role scp600");
            this.Config.ScpRole.Register();
            Scp600v.RegisteredInstance = this.Config.ScpRole;
            ApperaceableRoles = ((RoleTypeId[])Enum.GetValues(typeof(RoleTypeId))).Where(x => x.IsHuman() && x != RoleTypeId.Tutorial && x != RoleTypeId.CustomRole && x != RoleTypeId.Flamingo && x != RoleTypeId.AlphaFlamingo && x != RoleTypeId.ZombieFlamingo).ToArray();
            Log.Debug($"{nameof(OnEnabled)}: Possible apperance role [{string.Join(", ", ApperaceableRoles)}]");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Debug($"{nameof(OnDisabled)}: Unregister role scp600");
            this.Config.ScpRole.Unregister();
            Scp600v.RegisteredInstance = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}