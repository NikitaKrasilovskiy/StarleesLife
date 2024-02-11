using MiniGames.Common.Script.Enums;
using MiniGames.TheLibrarian.Scripts;
using UnityEngine;
public class Shelf : MonoBehaviour
{
    public bool areBooksRight;

    public BookTypes shelfType;

    private void Start()
    {
        CheckBooks();
    }

    public bool CheckBooks()
    {
        areBooksRight = true;

        for (var i = 1; i < transform.childCount; i++)
        {
            var bookScript = transform.GetChild(i).gameObject.GetComponent<Book>();

            if (bookScript.bookType != shelfType)
            {
                areBooksRight = false;
            }
        }

        return areBooksRight;
    }
}
