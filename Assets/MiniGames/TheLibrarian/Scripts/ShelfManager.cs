using System;
using System.Collections;
using System.Collections.Generic;
using MiniGames.TheLibrarian.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ShelfManager : MonoBehaviour
{
    private bool victory;
    public bool Victory => victory;
    private bool defeat;

    private int turnsRemaining;
    private bool gameStarted;

    private int shuffles;
    [SerializeField] private List<GameObject> books;
    [SerializeField] private List<GameObject> chosenBooks;
    [SerializeField] private List<GameObject> chosenBooksReset;

    [SerializeField] private string ObjectiveText;
    [SerializeField] private string DefeatText;
    [SerializeField] private string VictoryText;

    public UnityEvent<string> onShuffleEnd;
    public UnityEvent<string> onExchangeEnd;
    public UnityEvent<string> onVictory;
    public UnityEvent<string> onDefeat;
    public UnityEvent<string> onGameStart;
    public UnityEvent onMiniGameClose;
    private bool _firstStart;
    
    [SerializeField] private Transform levelLoaded;

    private void OnEnable()
    {
        foreach (Transform level in transform)
        {
            level.gameObject.SetActive(false);
        }
        
        StartGame();
    }

    private void OnDisable()
    {
        chosenBooks.Clear();
    }

    private void StartGame()
    {
        gameStarted = false;
        
        levelLoaded = transform.GetChild(Random.Range(0, transform.childCount));
        //levelLoaded = transform.GetChild(0);
        levelLoaded.gameObject.SetActive(true);
        
        GetBooks();

        SetBookPositions();
        ShuffleBooks();
        
        gameStarted = true;
    }

    private void GetBooks()
    {
        books.Clear();

        foreach (Transform shelf in levelLoaded)
        {
            for (var i = 1; i < shelf.childCount; i++)
            {
                books.Add(shelf.GetChild(i).gameObject);
            }
        }
    }

    private void SetBookPositions()
    {
        foreach (GameObject book in books)
        {
            book.GetComponent<Book>().currentPosition = book.transform.position;
        }
    }

    public void Reset()
    {
        gameStarted = false;
        
        GetBooks();

        foreach (var book in books)
        {
            book.GetComponent<Book>().currentPosition = book.GetComponent<Book>().startingPosition;
            book.transform.parent = book.GetComponent<Book>().startingParent;
        }

        ShuffleBooks();
        
        gameStarted = true;
    }
    
    
    public void ShuffleBooks()
    {
        chosenBooks.Clear();
        
        turnsRemaining = 0;
        
        victory = false;
        defeat = false;
        
        shuffles = levelLoaded.GetComponent<Level>().shuffles;

        for (var i = 0; i < shuffles; i++)
        {
            //Get random book and its shelf
            
            int bookId = Random.Range(0, books.Count);
            Transform shelf = books[bookId].transform.parent;

            GameObject firstBook = books[bookId];
            
            ChooseBook(books[bookId]);
            books.Remove(books[bookId]);
            
            //Find other shelves
            List<Transform> otherShelves = new List<Transform>();

            foreach (Transform otherShelf in levelLoaded)
            {
                if (otherShelf != shelf)
                {
                    otherShelves.Add(otherShelf);
                }
            }
            
            //Get shelve where are less than half of a books have a same color

            Transform shelfToUse  = otherShelves[0];

            foreach (Transform newShelf in otherShelves)
            {
                shelfToUse = newShelf;
                
                float bookOnShelfQuantity = 0;
                float booksOfTheSameColor = 0;
                
                for (var j = 1; j < shelfToUse.childCount; j++)
                {
                    bookOnShelfQuantity++;
                    
                    if (newShelf.GetChild(j).GetComponent<Book>().bookType ==
                        firstBook.GetComponent<Book>().bookType)
                    {
                        booksOfTheSameColor++;
                    }
                }

                if (booksOfTheSameColor < Math.Floor(bookOnShelfQuantity / 2f))
                {
                    break;
                }
            }
            
            //Get random book of from that shelf

            foreach (Transform book in shelfToUse)
            {
                if (book.GetComponent<Book>())
                {
                    if (book.GetComponent<Book>().bookType != firstBook.GetComponent<Book>().bookType)
                    {
                        turnsRemaining++;
                        ChooseBook(book.gameObject);
                        books.Remove(book.gameObject);
                        break;
                    }
                }
            }
        }

        onShuffleEnd.Invoke(turnsRemaining.ToString());
        onGameStart.Invoke(ObjectiveText);
    }

    public void ChooseBook(GameObject book)
    {
        chosenBooks.Add(book);
        chosenBooksReset.Add(book);
        
        if (chosenBooks.Count >= 2)
        {
            //Exchange shelves
            
            var firstShelf = chosenBooks[0].transform.parent;
            var secondShelf = chosenBooks[1].transform.parent;

            var tempShelf = firstShelf;
            chosenBooks[0].transform.parent = secondShelf;
            chosenBooks[1].transform.parent = tempShelf;

            //Exchange positions
            
            var firstBook = chosenBooks[0].GetComponent<Book>();
            var secondBook = chosenBooks[1].GetComponent<Book>();

            var tempPosition = firstBook.currentPosition;
             firstBook.currentPosition = secondBook.currentPosition;
             secondBook.currentPosition = tempPosition;

            //Reset book choosing state
            
            firstBook.Chosen(false);
            secondBook.Chosen(false);
            
            chosenBooks.Clear();

            //Check if shelves are fully completed
            
            firstShelf.GetComponent<Shelf>().CheckBooks();
            secondShelf.GetComponent<Shelf>().CheckBooks();
            
            StartCoroutine(CloseMiniGame());
            
            StartCoroutine(ResetAnim());
            
            if (gameStarted)
            {
                turnsRemaining--;
                
                if (turnsRemaining < 0)
                {
                    turnsRemaining = 0;
                }
                
                onExchangeEnd.Invoke(turnsRemaining.ToString());
                
                CheckIfWon();
                
                if (turnsRemaining == 0 && !victory)
                {
                    defeat = true;
                    onDefeat.Invoke(DefeatText);
                    StartCoroutine(CloseMiniGame());
                }
            }
        }
    }

    private void CheckIfWon()
    {
        var quantityOfShelves = 0;
        var quantityOfFullShelves = 0;
            
        //Check it full and check if all shelves are good
        foreach (Transform shelf in levelLoaded)
        {
            quantityOfShelves++;
                
            if (shelf.GetComponent<Shelf>().areBooksRight)
            {
                quantityOfFullShelves++;
            }
        }

        if (quantityOfShelves == quantityOfFullShelves && !defeat)
        {
            victory = true;
            onVictory.Invoke(VictoryText);
            StartCoroutine(CloseMiniGame());
        }
    }
    
    private IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(0.3f);
        if (chosenBooksReset.Count >= 2)
        {
            chosenBooksReset[0].GetComponent<Book>().Chosen(false);
            chosenBooksReset[1].GetComponent<Book>().Chosen(false);
            chosenBooksReset.Clear();
        }
    }
    
    private IEnumerator CloseMiniGame()
    {
        yield return new WaitForSeconds(3);
        onMiniGameClose.Invoke();
    }
}