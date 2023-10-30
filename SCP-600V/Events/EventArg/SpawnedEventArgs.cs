using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SCP_600V.Events.EventArg
{
    public class SpawnedEventArgs: EventArgs
    {
        /// <summary>
        /// player who is scp 600
        /// </summary>
        public Player Player { get; private set; }
        /// <summary>
        /// Position where scp 600 will appear
        /// </summary>
        public Vector3 Position { get; private set; }
        /// <summary>
        /// Maximum Health
        /// </summary>
        public int MaxHealt { get; private set; } = 400;
        /// <summary>
        /// Health
        /// </summary>
        public float Healt { get; private set; } = 400f;
        /// <summary>
        /// the role the player had before being called
        /// </summary>
        public RoleTypeId PlayerOldRole { get; private set; }

        public SpawnedEventArgs(Player player, Vector3 position, int maxHealt, float healt, RoleTypeId role)
        {
            Player = player;
            Position = position;
            MaxHealt = maxHealt;
            Healt = healt;
            PlayerOldRole = role;
        }
    }
}
