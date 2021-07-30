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
    private GameObject startParent;

    private void Awake()
    {
        canvas = GameObject.Find("Main Canvas");
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

        if(isOverDropZone && dropZone.CompareTag("Stack"))
        {
            transform.SetParent(dropZone.transform, false);
        }
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform, false);
        }
    }
}
