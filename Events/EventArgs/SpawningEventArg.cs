using Exiled.API.Features;

namespace SCP_600V.Events.EventArgs
{
    public class SpawningEventArg: System.EventArgs
    {
        public Player Player { get; set; }

        public float MaxHealt { get; }

        public bool IsAllow { get; set; }

        public SpawningEventArg(Player player, float maxHealt, bool isAllow)
        {
            Player = player;
            MaxHealt = maxHealt;
            IsAllow = isAllow;
        }
    }
}
