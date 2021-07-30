using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject previewArea;

    private GameObject zoomCard;

    public void Awake()
    {
        
        previewArea = GameObject.Find("Preview Area");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(previewArea.transform.position.x, previewArea.transform.position.y), Quaternion.identity);
        zoomCard.transform.SetParent(previewArea.transform, true);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }

}
