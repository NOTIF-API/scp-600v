using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_600V.API.Server
{
    public class ServerSetting
    {
        /// <summary>
        /// Send information if my plugin off frindly fire
        /// </summary>
        /// <returns>bool value</returns>
        public static bool IsFFCaptured()
        {
            return Sai.Instance.Config.IsFFEnabled;
        }
    }
}
