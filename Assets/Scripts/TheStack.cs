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
                //When the card is on the stack if there is not enough mana in the pool and the card is not marked as paid, return to hand.
                else if (
                    (((gameManagerScript.playerManaRed - lastScript.card.redManaCost) < 0) 
                    || ((gameManagerScript.playerManaBlue - lastScript.card.blueManaCost) < 0) 
                    || ((gameManagerScript.playerManaGreen - lastScript.card.greenManaCost) < 0) 
                    || ((gameManagerScript.playerManaBlack - lastScript.card.blackManaCost) < 0)
                    || ((gameManagerScript.playerManaWhite - lastScript.card.whiteManaCost) < 0)
                    || ((gameManagerScript.playerManaColorless - lastScript.card.colorlessManaCost) < 0)
                    || ((gameManagerScript.playerManaRed + gameManagerScript.playerManaBlue + gameManagerScript.playerManaGreen + gameManagerScript.playerManaBlack + gameManagerScript.playerManaWhite + gameManagerScript.playerManaColorless) < lastScript.card.cmc))
                    && lastScript.manaPaid == false
                    )
                {
                    Debug.Log("You don't have enough mana!");
                    gameManagerScript.timesPassed = tempTimesPassed;
                    lastScript.wasCastThisPhase = false;
                    lastTransform.SetParent(playerHand.transform, false);
                }
                //If the mana has not already been paid automatically spend the colored mana then automatically spend the remaing mana cost in this order: crugbw, then mark as paid.
                else if(lastScript.manaPaid == false)
                {
                    gameManagerScript.addToManaPool('c', "Player", -lastScript.card.colorlessManaCost);
                    gameManagerScript.addToManaPool('r', "Player", -lastScript.card.redManaCost);
                    gameManagerScript.addToManaPool('u', "Player", -lastScript.card.blueManaCost);
                    gameManagerScript.addToManaPool('g', "Player", -lastScript.card.greenManaCost);
                    gameManagerScript.addToManaPool('b', "Player", -lastScript.card.blackManaCost);
                    gameManagerScript.addToManaPool('w', "Player", -lastScript.card.whiteManaCost);

                    int remainingCost = lastScript.card.genericManaCost;

                    while (remainingCost > 0)
                    {
                        if(gameManagerScript.playerManaColorless > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('c', "Player", -1);
                        }
                        else if (gameManagerScript.playerManaRed > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('r', "Player", -1);
                        }
                        else if (gameManagerScript.playerManaBlue > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('u', "Player", -1);
                        }
                        else if (gameManagerScript.playerManaGreen > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('g', "Player", -1);
                        }
                        else if (gameManagerScript.playerManaBlack > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('b', "Player", -1);
                        }
                        else if (gameManagerScript.playerManaWhite > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('w', "Player", -1);
                        }
                    }

                    lastScript.manaPaid = true;


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
                //When the card is on the stack if there is not enough mana in the pool and the card is not marked as paid, return to hand.
                else if (
                    (((gameManagerScript.enemyManaRed - lastScript.card.redManaCost) < 0)
                    || ((gameManagerScript.enemyManaBlue - lastScript.card.blueManaCost) < 0)
                    || ((gameManagerScript.enemyManaGreen - lastScript.card.greenManaCost) < 0)
                    || ((gameManagerScript.enemyManaBlack - lastScript.card.blackManaCost) < 0)
                    || ((gameManagerScript.enemyManaWhite - lastScript.card.whiteManaCost) < 0)
                    || ((gameManagerScript.enemyManaColorless - lastScript.card.colorlessManaCost) < 0)
                    || ((gameManagerScript.enemyManaRed + gameManagerScript.enemyManaBlue + gameManagerScript.enemyManaGreen + gameManagerScript.enemyManaBlack + gameManagerScript.enemyManaWhite + gameManagerScript.enemyManaColorless) < lastScript.card.cmc))
                    && lastScript.manaPaid == false
                    )
                {
                    Debug.Log("You don't have enough mana!");
                    gameManagerScript.timesPassed = tempTimesPassed;
                    lastScript.wasCastThisPhase = false;
                    lastTransform.SetParent(enemyHand.transform, false);
                }
                //If the mana has not already been paid automatically spend the colored mana then automatically spend the remaing mana cost in this order: crugbw, then mark as paid.
                else if (lastScript.manaPaid == false)
                {

                    gameManagerScript.addToManaPool('c', "Enemy", -lastScript.card.colorlessManaCost);
                    gameManagerScript.addToManaPool('r', "Enemy", -lastScript.card.redManaCost);
                    gameManagerScript.addToManaPool('u', "Enemy", -lastScript.card.blueManaCost);
                    gameManagerScript.addToManaPool('g', "Enemy", -lastScript.card.greenManaCost);
                    gameManagerScript.addToManaPool('b', "Enemy", -lastScript.card.blackManaCost);
                    gameManagerScript.addToManaPool('w', "Enemy", -lastScript.card.whiteManaCost);

                    int remainingCost = lastScript.card.genericManaCost;

                    while (remainingCost > 0)
                    {
                        if (gameManagerScript.enemyManaColorless > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('c', "Enemy", -1);
                        }
                        else if (gameManagerScript.enemyManaRed > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('r', "Enemy", -1);
                        }
                        else if (gameManagerScript.enemyManaBlue > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('u', "Enemy", -1);
                        }
                        else if (gameManagerScript.enemyManaGreen > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('g', "Enemy", -1);
                        }
                        else if (gameManagerScript.enemyManaBlack > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('b', "Enemy", -1);
                        }
                        else if (gameManagerScript.enemyManaWhite > 0)
                        {
                            remainingCost--;
                            gameManagerScript.addToManaPool('w', "Enemy", -1);
                        }
                    }

                    lastScript.manaPaid = true;


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
