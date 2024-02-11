using System.Collections;
using UnityEngine;

public class ColliderCat : MonoBehaviour
{
    public int index;
    private GameManager gameManager;
    void Start()
    { gameManager = FindObjectOfType<GameManager>(); }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MouseMove>().index == index)
        { 
            other.gameObject.GetComponent<MouseMove>().MouseDown = false;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Wait());
            gameManager.a++;
        }

        if (other.gameObject.GetComponent<MouseMove>().index != index)
        { other.gameObject.GetComponent<MouseMove>().MouseDown = false; gameManager.i--; }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.5f);
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
}
