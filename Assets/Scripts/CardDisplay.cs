using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerDownHandler
{
    public Card card;
    public GameObject playerLandArea;
    public GameObject playerBattlefield;
    public GameObject enemyLandArea;
    public GameObject enemyBattlefield;
    public bool wasCastThisPhase = false;

    private Image image;
    private RectTransform cardRotation;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        cardRotation = gameObject.GetComponent<RectTransform>();

        image = gameObject.GetComponent<Image>();

        image.sprite = card.cardArt;
    }

    private void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.parent.name == "Player Land Area")
        {
            if (cardRotation.rotation.z == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);

                if (card.cardSubtype1 == "Island" || card.cardSubtype2 == "Island")
                {
                    Debug.Log("This is an island!");
                    gameManagerScript.addToManaPool('u', "Player");
                }

                if (card.cardSubtype1 == "Swamp" || card.cardSubtype2 == "Swamp")
                {
                    Debug.Log("This is a swamp!");
                    gameManagerScript.addToManaPool('b', "Player");
                }
            }

        }
        else if (transform.parent.name == "Enemy Land Area")
        {
            if (cardRotation.rotation.z == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);

                if (card.cardSubtype1 == "Island" || card.cardSubtype2 == "Island")
                {
                    Debug.Log("This is an island!");
                    gameManagerScript.addToManaPool('u', "Enemy");
                }

                if (card.cardSubtype1 == "Swamp" || card.cardSubtype2 == "Swamp")
                {
                    Debug.Log("This is a swamp!");
                    gameManagerScript.addToManaPool('b', "Enemy");
                }
            }

        }
        else if ((transform.parent.name == "Player Battlefield" && gameManagerScript.phase == "Attackers") || (transform.parent.name == "Enemy Battlefield" && gameManagerScript.phase == "Attackers Opp"))
        {
            if (cardRotation.rotation.z == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }


    }

}
