using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Goryned
{
    namespace Core
    {
        public static class LoadChecker
        {
            public enum LoadStepType
            {
                SceneLoaded,
                PlayFabLogged,
                ProfileLoaded,
                UserDataLoaded,
                TitleDataLoaded,
                StatisticsLoaded,
            }

            public static Dictionary<LoadStepType, bool> LoadSteps;

            public static bool IsAllReady(bool withDebug = true)
            {
                foreach (var item in LoadSteps)
                {
                    if (item.Value == false)
                    {
                        if (withDebug) Debug.Log(item);
                        return false;
                    }
                }
                return true;
            }

            public static void Reset()
            {
                LoadSteps = new Dictionary<LoadStepType, bool>();
                foreach (LoadStepType loadStep in Enum.GetValues(typeof(LoadStepType)))
                {
                    LoadSteps.Add(loadStep, false);
                }
            }

            public static void Complete(LoadStepType loadStepType)
            {
                if (LoadSteps.ContainsKey(loadStepType)) LoadSteps[loadStepType] = true;
            }

            public static void ShowProgress(float progress, TMP_Text textField = null, Slider slider = null, bool withDebug = false)
            {
                int percent = (int)(progress * 100);
                if (withDebug) Debug.Log("Progress: " + percent);
                if (textField != null) textField.text = "Progress: " + percent.ToString();
                if (slider != null) slider.value = progress;
            }
        }
    }
}
