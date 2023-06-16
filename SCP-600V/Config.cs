using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
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
        public int PercentToSpawn { get; set; } = 25;
        /// <summary>
        /// determines if SCP-600 will take damage over its lifetime
        /// </summary>
        [Description("determines if SCP-600 will take damage over its lifetime")]
        public bool CanBleading { get; set; } = true;
        /// <summary>
        /// Badge color in player list
        /// </summary>
        [Description("Badge color in player list only lowers chars")]
        public string BadgeColor { get; set; } = "red";
        /// <summary>
        /// message when admin gets list of scp players
        /// </summary>
        [Description("message when admin gets list of scp players")]
        public string ListGetted { get; set; } = "Players: {name} | now playing as SCP-600V";
        /// <summary>
        /// message when admin doesn't have command rights
        /// </summary>
        [Description("message when admin doesn't have command rights")]
        public string PermissionDenied { get; set; } = "Problems with your's permissions";
        /// <summary>
        /// player search error message
        /// </summary>
        [Description("player search error message")]
        public string PlayerNF { get; set; } = "Player not found";
        /// <summary>
        /// message when admin changed max hp
        /// </summary>
        [Description("message when admin changed max hp")]
        public string MhpChenged { get; set; } = "your max healt set: {amount} and healed";
        /// <summary>
        /// error in determining the player's health
        /// </summary>
        [Description("error in determining the player's health")]
        public string MhpHealArgEr { get; set; } = "i can't find out how much health you need to give";
        /// <summary>
        /// message when admin cannot become scp-600
        /// </summary>
        [Description("message when admin cannot become scp-600")]
        public string SpawnCommandEr { get; set; } = "you cannot become scp-600 because you are an observer";
        /// <summary>
        /// message if player dead by scp-600
        /// </summary>
        [Description("message if player dead by scp-600")]
        public string MessageDeathPlayerByScp600 { get; set; } = "your killed by <color=#FF0000>SCP-600V</color>";
        /// <summary>
        /// message if player last in game with other scp and more
        /// </summary>
        public string OnLastPlayer { get; set; } = "<align=\"left\"><color=#00FF00>Your</color> last player\n<align=\"center\"><color=FF0000>All human dead!</color>";
        public Scp600CotumRoleBase Scp600ConfigRole { get; set; } = new Scp600CotumRoleBase();
    }
}
