using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MiniGames.PlushRun.Scripts
{
    public class RoadManager : MonoBehaviour
    {
        [SerializeField] private Transform obstaclesParent;
        [SerializeField] private List<Transform> obstacles;
    
        [SerializeField] private int minSpawnTime;
        [SerializeField] private int maxSpawnTime;

        [SerializeField] private TextMeshProUGUI messageText;

        [SerializeField] private bool victory;
        [SerializeField] private bool defeat;
        
        [SerializeField] private string objectiveText;
        [SerializeField] private string victoryText;
        [SerializeField] private string defeatText;

        [SerializeField] private bool gameIsOn;
        
        [SerializeField] private UnityEvent<string> onDefeat;
        [SerializeField] private UnityEvent<string> onVictory;

        [SerializeField] private UnityEvent onMiniGameClose;

        private void OnEnable()
        {
            foreach (Transform obstacle in obstaclesParent)
            {
                obstacles.Add(obstacle);
            }

            messageText.text = objectiveText;

            victory = false;
            defeat = false;

            gameIsOn = true;

            StartCoroutine(SpawnObstacle());
        }

        private IEnumerator SpawnObstacle()
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            obstacles[Random.Range(0, obstacles.Count)].gameObject.SetActive(true);

            if (gameIsOn)
            {
                StartCoroutine(SpawnObstacle());
            }
        }

        public void Defeat()
        {
            if (!victory)
            {
                onDefeat.Invoke(defeatText);
                StartCoroutine(CloseMiniGame());
                defeat = true;
            }
        }

        public void Victory()
        {
            if (!defeat)
            {
                onVictory.Invoke(victoryText);
                StartCoroutine(CloseMiniGame());
                victory = true;
            }
        }
        
        private IEnumerator CloseMiniGame()
        {
            gameIsOn = false;
            
            yield return new WaitForSeconds(3);

            foreach (Transform obstacle in obstacles)
            {
                obstacle.GetComponent<Obstacle>().Reset();
            }

            onMiniGameClose.Invoke();
        }
    }
}
