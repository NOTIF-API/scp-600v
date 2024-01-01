using System.ComponentModel;
using Exiled.API.Interfaces;
using SCP_600V.Extension;

namespace SCP_600V
{
    internal class Config : IConfig
    {
        /// <summary>
        /// Enable or disable plugin
        /// </summary>
        [Description("Enable or disable plugin")]
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// Can the player see the debug message in the server console
        /// </summary>
        [Description("Can the player see the debug message in the server console")]
        public bool Debug { get; set; } = false;
        /// <summary>
        /// probability of occurrence at the beginning of the game in percentages.
        /// </summary>
        /// <summary>
        /// determines if SCP-600 will take damage over its lifetime
        /// </summary>
        [Description("determines if SCP-600 will take damage over its lifetime")]
        public bool CanBleading { get; set; } = true;
        /// <summary>
        /// Badge color in player list
        /// </summary>
        [Description("Badge color in player list")]
        public string BadgeColor { get; set; } = "red";
        /// <summary>
        /// message when admin gets list of scp players
        /// </summary>
        [Description("message when admin gets list of scp players")]
        public string ListGetted { get; set; } = "Players: {name} | now playing as SCP-600V";
        /// <summary>
        /// SCP-600V Role Configurations
        /// </summary>
        [Description("SCP-600V Role Configurations")]
        public Scp600Base Scp600ConfigRole { get; set; } = new Scp600Base();
    }
}
