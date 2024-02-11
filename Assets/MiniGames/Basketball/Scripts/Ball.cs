using System.Collections;
using UnityEngine;

namespace MiniGames.Basketball.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private bool held;

        [SerializeField] private Transform spawner;
    
        [SerializeField] private Camera mainCamera;

        [SerializeField] private BasketballManager bManager;

        [SerializeField] private bool goal;
    
        private bool flung;
    
        private Rigidbody2D rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;
            flung = false;
            transform.position = spawner.position;
            goal = false;
        }

        private void Update()
        {
            if(held)
            {
                rb.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        private void OnMouseDown()
        {
            if (!flung)
            {
                held = true;
                rb.isKinematic = true;
            }
        }

        private void OnMouseUp()
        {
            if (!flung)
            {
                rb.isKinematic = false;
                held = false;

                StartCoroutine(Fling());  
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 1)
            {
                goal = true;
            }
        }

        private IEnumerator Fling()
        {
            yield return new WaitForSeconds(0.1f);
        
            flung = true;

            GetComponent<SpringJoint2D>().enabled = false;

            StartCoroutine(Reset(3));
        }

        private IEnumerator Reset(int seconds)
        {
            yield return new WaitForSeconds(seconds);
        
            bManager.ThrowEnded(goal);
        
            transform.position = spawner.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            flung = false;
        
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpringJoint2D>().enabled = true;
            goal = false;
        }
    }
}
