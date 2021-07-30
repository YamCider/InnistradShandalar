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

    private Image image;
    private RectTransform cardRotation;

    // Start is called before the first frame update
    void Start()
    {
        cardRotation = gameObject.GetComponent<RectTransform>();

        image = gameObject.GetComponent<Image>();

        image.sprite = card.cardArt;
    }

    private void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.parent.name == "Player Land Area" || transform.parent.name == "Enemy Land Area")
        {
            if (cardRotation.rotation.z == 0)
            {
                transform.Rotate(Vector3.forward, -90f);
            }

        }

        
    }

}
