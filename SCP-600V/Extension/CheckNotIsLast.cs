using Exiled.API.Features;
using PlayerRoles;
using System.Collections.Generic;
using roles = SCP_600V.API.Players.Servroles;
using sc = SCP_600V.API.Players.Scp600PlyGet;
using Item = Exiled.API.Features.Items;
using MEC;

namespace SCP_600V.Extension
{
    public class CheckNotIsLast
    {
        public static void Start()
        {
            int mtf = roles.TeamsUser(Team.FoundationForces);
            int classd = roles.TeamsUser(Team.ClassD);
            int scient = roles.TeamsUser(Team.Scientists);
            List<Item.Item> items = new List<Item.Item>();
            if (mtf == 0|| classd == 0|| scient == 0)
            {
                foreach (Player ply in sc.GetScp600())
                {
                    foreach (Item.Item item in ply.Items)
                    {
                        items.Add(item);
                    }
                    Timing.CallDelayed(0.3f, () =>
                    {
                        ply.Role.Set(RoleTypeId.Tutorial, Exiled.API.Enums.SpawnReason.ForceClass, RoleSpawnFlags.None);
                    });
                    ply.AddItem(items);
                }
            }
        }
    }
}
