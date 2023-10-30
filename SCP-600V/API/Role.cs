using Exiled.API.Features;
using MEC;
using PlayerRoles;
using SCP_600V.Extension;
using System.Collections.Generic;
using System.Linq;
using Exiled.CustomRoles.API.Features;
using SCP_600V.Events;

namespace SCP_600V.API.Role
{
    public class Role
    {
        /// <summary>
        /// gets a list of players with the Breeder role on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> Scp600Players() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsSCP600")).ToList();
        /// <summary>
        /// gets a list of players who have the Serpent's Hand role on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> SHPlayers() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsSH")).ToList();
        /// <summary>
        /// gets a list of players with the role of Mask of Obsession on the server
        /// </summary>
        /// <returns>Player list</returns>
        public static List<Player> Scp035Players() => Player.List.Where(x => x != null & x.SessionVariables.ContainsKey("IsScp035")).ToList();
        /// <summary>
        /// Gets a player and defines their role
        /// </summary>
        /// <param name="player">Player to check</param>
        /// <returns>returns true if the player is SCP-600 or not null</returns>
        public static bool IsScp600(Player player)
        {
            if (player != null & player.SessionVariables.ContainsKey("IsSCP600") | CustomRole.Get(typeof(Scp600Base)).Check(player))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gives the role of SCP-600V to the player
        /// </summary>
        /// <param name="player">The player who will receive the role of SCP-600V</param>
        /// <param name="MaxHealt">Assigning a custom MaxHealt parameter to a player</param>
        public static void Spawn(Player player)
        {
            if (player == null && player.IsDead)
            {
                return;
            }
            else
            {
                Events.EventArg.SpawningEventArgs e = new Events.EventArg.SpawningEventArgs(player, true);
                Log.Debug($"After Invoke event Player: {e.Player.Nickname} IsAllow: {e.IsAllow}");
                new Scp600Handler().InvokeSpawning(e);
                Log.Debug($"Before Invoke event Player: {e.Player.Nickname} IsAllow: {e.IsAllow}");

                if (e.IsAllow)
                {
                    CustomRole.Get(typeof(Scp600Base)).AddRole(e.Player);
                }
                if (e.IsAllow == false)
                {
                    Log.Debug("Player do not get role called event set IsSpawning to false");
                }
            }
        }
    }
}
