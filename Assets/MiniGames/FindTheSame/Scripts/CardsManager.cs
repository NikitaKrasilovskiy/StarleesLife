using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.FindTheSame.Scripts
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cards;
        [SerializeField] private List<GameObject> chosenCards;
        [SerializeField] private int startShowingCardsTime;
    
        [SerializeField] private bool gameStarted;

        [SerializeField] private int pairsToFind;
        [SerializeField] private int additionalTurns;
        private int turnsRemained;

        public List<GameObject> shownCards;

        private bool victory;
        public bool Victory => victory;
        
        private bool defeat;

        public UnityEvent<string> onGameStart;
        public UnityEvent<string> onVictory;
        public UnityEvent<string> onDefeat;
        public UnityEvent<string> onTurnsUpdate;
        public UnityEvent onMiniGameClose;
    
        [SerializeField] private string victoryText;
        [SerializeField] private string defeatText;
        [SerializeField] private string objectiveText;
    
        private void OnEnable()
        {
            victory = false;
            defeat = false;
        
            GetCards();

            pairsToFind = cards.Count / 2;
            turnsRemained = pairsToFind + additionalTurns;

            foreach (var card in cards)
            {
                card.SetActive(true);
            }
        
            ShuffleCards();
            GetCards();
            StartCoroutine(ShowCards());
            onTurnsUpdate.Invoke(turnsRemained.ToString());
            onGameStart.Invoke(objectiveText);
        }

        private IEnumerator ShowCards()
        {
            foreach (var card in cards)
            {
                card.GetComponent<Card>().Chosen(true);
            }
        
            yield return new WaitForSeconds(startShowingCardsTime);
        
            foreach (var card in cards)
            {
                card.GetComponent<Card>().Chosen(false);
            }

            shownCards.Clear();
            gameStarted = true;
        }

        private void GetCards()
        {
            cards.Clear();

            foreach (Transform card in transform)
            {
                cards.Add(card.gameObject);
            }
        }
    
        private void ShuffleCards()
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var tempPosition = cards[i].transform.position;
                var secondCard = cards[Random.Range(i+1, cards.Count)];
            
                cards[i].transform.position = secondCard.transform.position;
                secondCard.transform.position = tempPosition;

                cards.Remove(cards[i]);
                cards.Remove(secondCard);
            }
        }

        private void Update()
        {
            if (shownCards.Count == 2 && gameStarted)
            {
                var firstCard = chosenCards[0].GetComponent<Card>();
                var secondCard = chosenCards[1].GetComponent<Card>();

                if (firstCard.cardType == secondCard.cardType 
                    && firstCard != secondCard)
                {
                    pairsToFind--;

                    if (pairsToFind == 0 && !defeat)
                    {
                        victory = true;
                        onVictory.Invoke(victoryText);
                        StartCoroutine(CloseMiniGame());
                    }
                
                    foreach (var shownCard in shownCards)
                    {
                        shownCard.GetComponent<Animator>().SetTrigger("FallAway");
                    }
                }
                else
                {
                    firstCard.Chosen(false);
                    secondCard.Chosen(false);
                }
            
                turnsRemained--;
                onTurnsUpdate.Invoke(turnsRemained.ToString());
            
                if (turnsRemained == 0 && !victory)
                {
                    defeat = true;
                    onDefeat.Invoke(defeatText);
                    StartCoroutine(CloseMiniGame());
                }

                for (int i = 0; i < chosenCards.Count; i++)
                {
                    chosenCards[i].GetComponent<Card>()._button.SetActive(true);
                }
                
                chosenCards.Clear();
                shownCards.Clear();
            }
        }

        public void ChooseACard(GameObject card)
        {
            if (chosenCards.Count < 2 && gameStarted && !defeat && !victory)
            {
                chosenCards.Add(card);
                card.GetComponent<Card>().Chosen(true);
                card.GetComponent<Card>()._button.SetActive(false);
            }
        }

        private IEnumerator CloseMiniGame()
        {
            yield return new WaitForSeconds(3);
            onMiniGameClose.Invoke();
        }
    }
}
