using UnityEngine;
using UnityEngine.UI;

public class ButtonComparison : MonoBehaviour
{
    private RandomColors scriptRandomColors;
    void Start()
    { scriptRandomColors = FindObjectOfType<RandomColors>(); }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Image>().sprite == gameObject.GetComponent<Image>().sprite
            && other.gameObject.GetComponent<Rigidbody2D>().isKinematic == false
            && other.gameObject.GetComponent<Image>().sprite != scriptRandomColors.spriteOkError[1]
            && other.gameObject.GetComponent<Image>().sprite != scriptRandomColors.spriteOkError[0])
        { scriptRandomColors.a++; other.gameObject.GetComponent<Image>().sprite = scriptRandomColors.spriteOkError[1]; }

        else if (other.gameObject.GetComponent<Image>().sprite != gameObject.GetComponent<Image>().sprite
            && other.gameObject.GetComponent<Rigidbody2D>().isKinematic == false
            && other.gameObject.GetComponent<Image>().sprite != scriptRandomColors.spriteOkError[1]
            && other.gameObject.GetComponent<Image>().sprite != scriptRandomColors.spriteOkError[0])
        { scriptRandomColors.i--; other.gameObject.GetComponent<Image>().sprite = scriptRandomColors.spriteOkError[0]; }

        if (other.gameObject.GetComponent<CircleCollider2D>())
        { other.gameObject.SetActive(false); }
    }
}
