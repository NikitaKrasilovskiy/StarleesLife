using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Goryned
{
    namespace Core
    {
        public static class Data
        {
            static char separator1 = '=';
            static char separator2 = ':';

            public static string DictionaryToString(Dictionary<string, int> dictionary)
            {
                string strData = string.Join(separator1.ToString(), dictionary.Select(d => string.Format("{0}{1}{2}", d.Key, separator2, d.Value)));
                return strData;
            }
            public static Dictionary<string, int> StringToDictionary(string strData)
            {
                string[] keys = strData.Split((char)separator1);
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                foreach (var item in keys)
                {
                    string[] values = item.Split(separator2);
                    dictionary.Add(values[0], int.Parse(values[1]));
                }
                return dictionary;
            }

            public static string ReduceInt(int value, int digits = 2)
            {
                int length = value.ToString().Length;
                int count = Mathf.FloorToInt(length / 4);
                float floatValue = (float)Math.Round(value / Mathf.Pow(1000, count), count > 0 ? digits : 0);
                string strValue = string.Format("{0}{1}", floatValue, new string('k', count));
                return strValue;
            }
        }
    }
}
