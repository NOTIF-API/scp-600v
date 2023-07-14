using Exiled.API.Features;
using PlayerRoles;
using System.Linq;

namespace SCP_600V.API.Role
{
    /// <summary>
    /// Allows you to get information about the Team on the server
    /// </summary>
    public class TeamGet
    {
        /// <summary>
        /// Allows you to get the number of people who do not have the role Breeder on the server
        /// </summary>
        /// <param name="team">Team to check</param>
        /// <returns>returns the number of players in the team excluding scp</returns>
        public static int AmountTeamNotScp(Team team) => Player.List.Count(x => x != null && !x.SessionVariables.ContainsKey("IsSCP600") && x.Role.Team == team);
        /// <summary>
        /// Determine if a player who is an SCP is in the team
        /// </summary>
        /// <param name="team">Team to check</param>
        /// <returns>returns true if the SCP player is on the team</returns>
        public static bool ScpInTeam(Team team) => !Player.List.Any(x => x != null && x.Role.Team == team && x.SessionVariables.ContainsKey("IsSCP600"));
    }
}
