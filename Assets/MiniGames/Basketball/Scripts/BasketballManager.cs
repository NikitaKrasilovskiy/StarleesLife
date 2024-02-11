using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace MiniGames.Basketball.Scripts
{
    public class BasketballManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private GameObject basketballNet;

        private float ballDistanceFromCenterX;
        private float ballDistanceFromCenterY;
        private float netDistanceFromCenterX;
        private float netDistanceFromCenterY;

        [SerializeField] private bool ballIsOnRightSide;
        
        [SerializeField] private int maxThrows;
        private int throws;
        
        [SerializeField] private int goalsMax;
        private int goals;

        [SerializeField] private string defeatText;
        [SerializeField] private string victoryText;
        [SerializeField] private string objectiveText;

        public UnityEvent<string> onThrowsUpdate;
        public UnityEvent<string> onGoalsUpdate;
        
        public UnityEvent<string> onDefeat;
        public UnityEvent<string> onVictory;
        public UnityEvent<string> onStart;
        public UnityEvent onClose;

        void OnEnable()
        {
            throws = maxThrows;
            onThrowsUpdate.Invoke(throws.ToString());
            
            goals = goalsMax;
            onGoalsUpdate.Invoke(goals.ToString());
            
            onStart.Invoke(objectiveText);
            
            GeneratePositions();
        }
        
        private IEnumerator Close()
        {
            yield return new WaitForSeconds(3);
            onClose.Invoke();
        }

        private void GeneratePositions()
        {
            if (ballIsOnRightSide)
            {
                ballDistanceFromCenterX = Random.Range(150, 250);
                ballDistanceFromCenterY = Random.Range(0, -300);
        
                netDistanceFromCenterX = Random.Range(-250, -150);
                netDistanceFromCenterY = Random.Range(0, 300);
            }
            else
            {
                ballDistanceFromCenterX = Random.Range(-250, -150);
                ballDistanceFromCenterY = Random.Range(0, -300);
        
                netDistanceFromCenterX = Random.Range(150, 250);
                netDistanceFromCenterY = Random.Range(0, 300);
            }

            ball.GetComponent<RectTransform>().anchoredPosition = new Vector3(ballDistanceFromCenterX, ballDistanceFromCenterY, 0);
            basketballNet.GetComponent<RectTransform>().anchoredPosition = new Vector3(netDistanceFromCenterX, netDistanceFromCenterY, 0);

            ballIsOnRightSide = !ballIsOnRightSide;
        }

        public void ThrowEnded(bool didScore)
        {
            throws--;
            onThrowsUpdate.Invoke(throws.ToString());

            if (didScore)
            {
                goals--;
                onGoalsUpdate.Invoke(goals.ToString());
                
                GeneratePositions();
            }
            
            if (goals <= 0)
            {
                onVictory.Invoke(victoryText);
                StartCoroutine(Close());
            }
            else if (throws <= 0 && goals > 0)
            {
                onDefeat.Invoke(defeatText);
                StartCoroutine(Close());
            }
        }
    }
}
