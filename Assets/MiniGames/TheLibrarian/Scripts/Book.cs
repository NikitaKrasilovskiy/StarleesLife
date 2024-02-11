using MiniGames.Common.Script.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.TheLibrarian.Scripts
{
    public class Book : Clickable
    {
        public BookTypes bookType;

        public Vector3 startingPosition;
        public Transform startingParent;
    
        public Vector3 currentPosition;
        [SerializeField] private int flySpeed = 20;

        private bool _chosenOn;
        
        protected override void Awake()
        {
            base.Awake();
            
            startingParent = transform.parent;
        }

        private void Start()
        {
            startingPosition = transform.position;
        }

        private void Update()
        {
            if (transform.position != currentPosition && !_chosenOn)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, Time.deltaTime * flySpeed);
            }
            else if (transform.position != currentPosition && _chosenOn)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, Time.deltaTime * flySpeed);
            }
        }

        public void ChosenAnim()
        {
            Chosen(true);
            _chosenOn = true;
        }
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                chosen = true;
                Chosen(true);
                onClick.Invoke();
            }
        }
    }
}