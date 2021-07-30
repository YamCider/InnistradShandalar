using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    CardDisplay cardDisplayScript;
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

    //When cards are placed in the stack send them to the correct board zone.
    void Stack()
    {

        if (transform.childCount > 0)
        {
            cardDisplayScript = gameObject.GetComponentInChildren<CardDisplay>();
            cardTransforms = gameObject.GetComponentsInChildren<RectTransform>();

            if (gameObject.transform.GetChild(0).CompareTag("PlayerCard"))
            {
                if (cardDisplayScript.card.cardType == "Basic Land" && !gameManagerScript.landPerTurnPlayer && (gameManagerScript.phase == "Main1" || gameManagerScript.phase == "Main2"))
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(playerLandArea.transform, false);
                    gameManager.GetComponent<GameManager>().landPerTurnPlayer = true;
                }
                else if (cardDisplayScript.card.cardType == "Creature")
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(playerBattlefield.transform, false);
                }
                else
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(playerHand.transform, false);
                }
            }
            else if (gameObject.transform.GetChild(0).CompareTag("EnemyCard"))
            {
                if (cardDisplayScript.card.cardType == "Basic Land" && !gameManagerScript.landPerTurnEnemy && (gameManagerScript.phase == "Main1 Opp" || gameManagerScript.phase == "Main2 Opp"))
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(enemyLandArea.transform, false);
                    gameManager.GetComponent<GameManager>().landPerTurnEnemy = true;
                }
                else if (cardDisplayScript.card.cardType == "Creature")
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(enemyBattlefield.transform, false);
                }
                else
                {
                    cardTransforms[cardTransforms.Length - 1].SetParent(enemyHand.transform, false);
                }
            }
        }
    }
}
