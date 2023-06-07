using Exiled.API.Features;
using Exiled.API.Enums;
using MEC;
using UnityEngine;
using System.Collections.Generic;
using PlayerRoles;

namespace SCP_600V.Component
{
    internal class HurtingPerTime: MonoBehaviour
    {
        private CoroutineHandle _coroutineHandle;
        private Player _Player;
        private bool IScp = true;
        private void Awake()
        {
            Log.Debug("Initialiaze Hurting for scp-600");
            _Player = Player.Get(gameObject);
            _coroutineHandle = Timing.RunCoroutine(Hurter());
        }
        private IEnumerator<float> Hurter()
        {
            for (; ; )
            {
                IScp = Player.Get(gameObject).SessionVariables.ContainsKey("IsSCP600");
                if (_Player.Role == RoleTypeId.Spectator & IScp)
                {
                    IScp = false;
                    _Player.SessionVariables.Remove("IsSCP600");
                    _Player.SessionVariables.Remove("IsScp");
                }
                if (IScp)
                {
                    Log.Debug("Hurting player");
                    _Player.Hurt(5f, DamageType.Unknown);
                    yield return Timing.WaitForSeconds(5f);
                }
                else
                {
                    Log.Debug("Call break");
                    Timing.KillCoroutines(_coroutineHandle);
                    Destroy(this);
                    break;
                }
            }
            Destroy(this);
        }
    }
}
