using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goryned
{
    namespace Core
    {
        public static class DateTimeManager
        {
            public static void SaveDateTime(string key)
            {
                SaveDateTime(key, DateTime.UtcNow);
            }
            public static void SaveDateTime(string key, DateTime dateTime)
            {
                JsonDateTime jsonDateTime = dateTime;
                SaveDateTime(key, jsonDateTime);
            }
            public static void SaveDateTime(string key, JsonDateTime jsonDateTime)
            {
                PlayerPrefs.SetString(key, JsonUtility.ToJson(jsonDateTime));
            }

            public static DateTime GetDateTime(string key)
            {
                return GetJsonDateTime(key);
            }
            public static bool HasKey(string key)
            {
                return PlayerPrefs.HasKey(key);
            }
            public static JsonDateTime GetJsonDateTime(string key)
            {
                if (!HasKey(key)) SaveDateTime(key);
                string data = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<JsonDateTime>(data);
            }

            public static TimeSpan GetInterval(DateTime startTime, DateTime endTime)
            {
                return endTime - startTime;
            }
            public static TimeSpan GetInterval(DateTime startTime)
            {
                return DateTime.UtcNow - startTime;
            }
            public static float GetSeconds(DateTime startTime, DateTime endTime)
            {
                return (float)GetInterval(startTime, endTime).TotalSeconds;
            }
            public static float GetSeconds(DateTime startTime)
            {
                return (float)GetInterval(startTime).TotalSeconds;
            }

            public static DateTime GetDateTime(float addSeconds)
            {
                return DateTime.UtcNow.AddSeconds(addSeconds);
            }
        }

    }
}