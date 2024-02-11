using System;
using UnityEngine;

namespace MiniGames.PlushRun.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private Vector3 startingPosition;
        
        [SerializeField] private Transform runner;
        [SerializeField] private Transform obstacleRoadEnd;

        private void Start()
        {
            startingPosition = transform.position;
        }

        private void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
        }

        public void Reset()
        {
            gameObject.SetActive(false);

            transform.position = startingPosition;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform == runner)
            {
                transform.parent.parent.GetComponent<RoadManager>().Defeat();
            }
            else if (other.transform == obstacleRoadEnd)
            {
                Reset();
            }
        }
    }
}
