using Exiled.API.Features;

namespace SCP_600V.Events.EventArgs
{
    public class DiedEventArg: System.EventArgs
    {
        public Player Player { get; set; }

        public Player Killer { get; set; }

        public DiedEventArg(Player player, Player killer)
        {
            Player = player;
            Killer = killer;
        }
    }
}
