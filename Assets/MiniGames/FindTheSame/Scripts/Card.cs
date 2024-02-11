using MiniGames.Common.Script.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.FindTheSame.Scripts
{
    public class Card : MonoBehaviour
    {
        public CardTypes cardType;
        
        [SerializeField] Animator anim;

        public bool shown = true;
        public bool hidden = false;
    
        public UnityEvent<GameObject> onClick;
        public GameObject _button;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0) && hidden)
            {
                onClick.Invoke(gameObject);
            }
        }
    
        public void Chosen(bool value)
        {
            anim.SetBool("Chosen", value);
        }

        public void CardIsShown()
        {
            gameObject.transform.parent.GetComponent<CardsManager>().shownCards.Add(gameObject);
            hidden = false;
        }

        public void FellAway()
        {
            gameObject.SetActive(false);
        }

        public void Hidden()
        {
            hidden = true;
        }

        public void NotHidden()
        {
            hidden = false;
        }
    }
}
