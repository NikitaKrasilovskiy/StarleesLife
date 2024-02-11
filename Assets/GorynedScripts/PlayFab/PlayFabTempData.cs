using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goryned
{
    namespace PlayFab
    {
        public static class PlayFabTempData
        {
            public static LoginResult loginResult;
            public static string playFabID;
            public static PlayerProfileModel playerProfileModel;

            public static Dictionary<string, string> userData;
            public static Dictionary<string, int> playerStatistics;
            public static Dictionary<string, string> titleData;

            public static UpdateUserDataResult updateUserDataResult;
            public static UpdatePlayerStatisticsResult updatePlayerStatisticsResult;

        }
    }
}
