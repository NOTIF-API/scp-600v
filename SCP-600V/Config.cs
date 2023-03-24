using System;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;
namespace SCP_600V
{
    internal class Config: IConfig
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
        /// Can SCPs beat our
        /// </summary>
        [Description("Can SCPs beat our")]
        public bool IsScpCanDamageMe { get; set; } = false;
        /// <summary>
        /// Max healt
        /// </summary>
        [Description("Max healt")]
        public int Maxhealt { get; set; } = 400;
        /// <summary>
        /// Percentage to spawn stsp at the beginning of the game
        /// </summary>
        [Description("Percentage to spawn stsp at the beginning of the game")]
        public int PercentToSpawn { get; set; } = 10;
        /// <summary>
        /// Spawn message for SCP-600V
        /// </summary>
        [Description("Spawn message for SCP-600V")]
        public string SpawnMessage { get; set; } = "You appeared behind SCP-600V";
        /// <summary>
        /// Badge color in player list
        /// </summary>
        [Description("Badge color in player list")]
        public string BadgeColor { get; set; } = "red";
        /// <summary>
        /// message when entering a command to change the player's class when he did not specify a class argument
        /// </summary>
        [Description("message when entering a command to change the player's class when he did not specify a class argument")]
        public string ClassType { get; set; } = "Set class name: {name}";
        /// <summary>
        /// message when entering a command when a player has reached the limit in reincarnations in the game
        /// </summary>
        [Description("message when entering a command when a player has reached the limit in reincarnations in the game")]
        public string AtempLimitMessage { get; set; } = "Wait next game";
        /// <summary>
        /// message when entering a command when the player is not an object
        /// </summary>
        [Description("message when entering a command when the player is not an object")]
        public string NotScp600 { get; set; } = "Your are not scp-600v now";
        /// <summary>
        /// message when player changed class
        /// </summary>
        [Description("message when player changed class")]
        public string CompleteTransform { get; set; } = "Done";
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
        /// message to the player when he transforms into another player
        /// </summary>
        [Description("message to the player when he transforms into another player")]
        public string MessageScpTransform { get; set; } = "you transformed into {player}";
    }
}
