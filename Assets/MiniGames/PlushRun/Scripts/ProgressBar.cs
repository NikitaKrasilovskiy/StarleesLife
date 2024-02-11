using System;
using UnityEngine;

namespace MiniGames.PlushRun.Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Vector3 startingPosition;
        [SerializeField] private Transform runner;
        [SerializeField] private Transform runnerTarget;
        [SerializeField] private float progressSpeed;
        [SerializeField] private float progress;

        [SerializeField] private RoadManager roadManager;
        
        private void Start()
        {
            startingPosition = runner.position;
        }

        private void FixedUpdate()
        {
            progress = progress + progressSpeed;
            runner.position = Vector3.Lerp(startingPosition, runnerTarget.position, progress);

            if (progress >= 1)
            {
                roadManager.GetComponent<RoadManager>().Victory();
            }
        }

        public void Reset()
        {
            progress = 0;
        }
    }
}
