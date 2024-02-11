using System;

using UnityEngine;

namespace MiniGames.PlushRun.Scripts
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Transform runner;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var runnerPosition = runner.transform.position;

                runner.transform.position = new Vector3(transform.position.x, runnerPosition.y, runnerPosition.z);
            }
        }
    }
}
