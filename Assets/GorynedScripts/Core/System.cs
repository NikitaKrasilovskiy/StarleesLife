using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goryned
{
    namespace Core
    {
        public static class System
        {
            public static string GetDeviceID(bool current = true)
            {
                string deviceID = SystemInfo.deviceUniqueIdentifier;
                if (current) return SystemInfo.deviceUniqueIdentifier;
                else return string.Format("{0}_{1}", deviceID, Random.Range(0, 10000));
            }
            public static AnimationClip GetAnimationClip(Animator animator)
            {
                return animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            }
            public static string GetAnimationClipName(Animator animator)
            {
                return GetAnimationClip(animator).name;
            }

            public static void ClearChildren(Transform parent)
            {
                for (int i = parent.childCount; i >= 0; i--)
                {
                    GameObject.Destroy(parent.GetChild(i).gameObject);
                }
            }
        }
    }
}
