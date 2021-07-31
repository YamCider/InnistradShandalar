using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    CardDisplay[] cardDisplayScripts;
    RectTransform[] cardTransforms;
    
    public GameObject playerLandArea;
    public GameObject playerBattlefield;
    public GameObject playerHand;
    public GameObject enemyLandArea;
    public GameObject enemyBattlefield;
    public GameObject enemyHand;
    public GameObject gameManager;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Stack();
    }

    //Sends cards back to the respective players hand if they are not supposed to be there.
    void Stack()
    {

        if (transform.childCount > 0)
        {
            cardDisplayScripts = gameObject.GetComponentsInChildren<CardDisplay>();
            cardTransforms = gameObject.GetComponentsInChildren<RectTransform>();

            CardDisplay lastScript = cardDisplayScripts[cardDisplayScripts.Length - 1];
            RectTransform lastTransform = cardTransforms[cardTransforms.Length - 1];

            //If the card in the stack is a Player Card execute this code.
            if (transform.GetChild(transform.childCount - 1).CompareTag("PlayerCard"))
            {
                //Store the amount of times passed so that if that spell is not supposed to be in the stack we can revert to this number.
                int tempTimesPassed = gameManagerScript.timesPassed;

                //If it is the players priority and we havent run this code before reset priority count to 0 and and mark this card so it isnt cleared from the stack when the enemy gains priority.
                if(gameManagerScript.priority == "Player" && lastScript.wasCastThisPhase == false)
                {
                    lastScript.wasCastThisPhase = true;
                    gameManagerScript.timesPassed = 0;
                }

                //If it is not the players priority and this card was not marked as cast this turn put it back to hand.
                if(gameManagerScript.priority != "Player" && lastScript.wasCastThisPhase == false)
                {
                    lastTransform.SetParent(playerHand.transform, false);
                }
                //If the spells stack speed isn't instant and we are not in the main phase with an empty stack revert the card to hand. Also revert to original priority count.
                else if (lastScript.card.stackSpeed != "Instant" && ((gameManagerScript.phase != "Main1" && gameManagerScript.phase != "Main2") || gameObject.transform.childCount > 1))
                {
                    gameManagerScript.timesPassed = tempTimesPassed;
                    lastScript.wasCastThisPhase = false;
                    lastTransform.SetParent(playerHand.transform, false);
                }
                
            }
            //If the card in the stack is an Enemy Card execute this code.
            else if (transform.GetChild(transform.childCount - 1).CompareTag("EnemyCard"))
            {
                //Store the amount of times passed so that if that spell is not supposed to be in the stack we can revert to this number.
                int tempTimesPassed = gameManagerScript.timesPassed;

                //If it is the enemies priority and we havent run this code before reset priority count to 0 and and mark this card so it isnt cleared from the stack when the player gains priority.
                if (gameManagerScript.priority == "Enemy" && lastScript.wasCastThisPhase == false)
                {
                    lastScript.wasCastThisPhase = true;
                    gameManagerScript.timesPassed = 0;
                }

                //If it is not the enemies priority and this card was not marked as cast this turn put it back to hand.
                if (gameManagerScript.priority != "Enemy" && lastScript.wasCastThisPhase == false)
                {
                    lastTransform.SetParent(enemyHand.transform, false);
                }
                //If the spells stack speed isn't instant and we are not in the main phase with an empty stack revert the card to hand. Also revert to original priority count.
                else if (lastScript.card.stackSpeed != "Instant" && ((gameManagerScript.phase != "Main1 Opp" && gameManagerScript.phase != "Main2 Opp") || gameObject.transform.childCount > 1))
                {
                    gameManagerScript.timesPassed = tempTimesPassed;
                    lastScript.wasCastThisPhase = false;
                    lastTransform.SetParent(enemyHand.transform, false);
                }

            }
        }
    }

    //Resolves the top spell in the stack.
    public void ResolveStack()
    {
        cardDisplayScripts = gameObject.GetComponentsInChildren<CardDisplay>();
        cardTransforms = gameObject.GetComponentsInChildren<RectTransform>();

        CardDisplay lastScript = cardDisplayScripts[cardDisplayScripts.Length - 1];
        RectTransform lastTransform = cardTransforms[cardTransforms.Length - 1];

        if (transform.GetChild(transform.childCount - 1).CompareTag("PlayerCard"))
        {
            if (lastScript.card.cardType == "Creature")
            {
                lastTransform.SetParent(playerBattlefield.transform, false);
            }
        }
        else if (transform.GetChild(transform.childCount - 1).CompareTag("EnemyCard"))
        {
            
            if (lastScript.card.cardType == "Creature")
            {
                lastTransform.SetParent(enemyBattlefield.transform, false);
            }
        }

    }
}
