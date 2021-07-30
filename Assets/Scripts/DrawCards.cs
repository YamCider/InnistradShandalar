using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    //Temporary way to add cards to the shared deck.
    public Card deck0;
    public Card deck1;
    public Card deck2;
    public Card deck3;


    public GameObject playerArea;
    public GameObject enemyArea;
    public GameObject gameManager;
    
    List<Card> deck = new List<Card>();
    

    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        //Temporary way to add cards to the "deck" list.
        deck.Add(deck0);
        deck.Add(deck1);
        deck.Add(deck2);
        deck.Add(deck3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the button is clicked deal the player and the opponent each 7 cards as copies randomly chosen from the shared "deck" list.
    public void OnClick()
    {
        for(int count = 0; count < 7; count++)
        {
            GameObject playerCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(playerArea.transform, false);
            playerCard.GetComponent<CardDisplay>().card = deck[Random.Range(0, deck.Count)];
            playerCard.tag = "PlayerCard";
        }

        
        for(int count = 0; count < 7; count++)
        {
            GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(enemyArea.transform, false);
            enemyCard.GetComponent<CardDisplay>().card = deck[Random.Range(0, deck.Count)];
            enemyCard.tag = "EnemyCard";
        }

        gameManager.GetComponent<GameManager>().StartGame();

    }
}
