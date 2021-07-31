using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 startPos;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    private GameObject canvas;
    private GameManager gameManagerScript;
    private GameObject startParent;

    private void Awake()
    {
        canvas = GameObject.Find("Main Canvas");
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        startParent = transform.parent.gameObject;
        startPos = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;

        //If the card is dragged over the Stack and it is not a land place it there.
        if (isOverDropZone && dropZone.CompareTag("Stack") && gameObject.GetComponent<CardDisplay>().card.cardType != "Basic Land" && gameObject.GetComponent<CardDisplay>().card.cardType != "Land")
        {
            transform.SetParent(dropZone.transform, false);
        }
        //If this card is dragged over the correct land area, is a land, is the first land per turn and it is the main phase, then place this card there and set land per turn to true.
        else if (
            isOverDropZone
            && dropZone.CompareTag("PlayerLandArea")
            && gameObject.CompareTag("PlayerCard")
            && (gameObject.GetComponent<CardDisplay>().card.cardType == "Basic Land" || gameObject.GetComponent<CardDisplay>().card.cardType == "Land")
            && (gameManagerScript.phase == "Main1" || gameManagerScript.phase == "Main2")
            && gameManagerScript.priority == "Player"
            && gameManagerScript.landPerTurnPlayer != true
            )
        {
            transform.SetParent(dropZone.transform, false);
            gameManagerScript.landPerTurnPlayer = true;
        }
        else if (
            isOverDropZone
            && dropZone.CompareTag("EnemyLandArea")
            && gameObject.CompareTag("EnemyCard")
            && (gameObject.GetComponent<CardDisplay>().card.cardType == "Basic Land" || gameObject.GetComponent<CardDisplay>().card.cardType == "Land")
            && (gameManagerScript.phase == "Main1 Opp" || gameManagerScript.phase == "Main2 Opp")
            && gameManagerScript.priority == "Enemy"
            && gameManagerScript.landPerTurnEnemy != true
            )
        {
            transform.SetParent(dropZone.transform, false);
            gameManagerScript.landPerTurnEnemy = true;
        }
        //If this card is dragged over an invalid position, revert it to the players hand.
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform, false);
        }
    }
}
