using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    public RectTransform _transform;
    public CanvasGroup canvasGroup;
    public Transform parentAfterDrag;
    public GraphicRaycaster graphicRaycaster;
    public bool itemHasBeenPlaced;
    public GameObject SellzoneRef;

    public Sprite ItemSprite;
    public string itemTag;
    public int marketPrice;

    public GameObject activeInventory;
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _transform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        SellzoneRef = GameObject.FindGameObjectWithTag("SellZone");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemHasBeenPlaced == false)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            transform.SetParent(canvas.transform, true);
            SellzoneRef.transform.GetChild(0).gameObject.SetActive(true);


        }


    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        _transform.position += (Vector3)eventData.delta * canvas.transform.localScale.x / canvas.scaleFactor;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stopped Drag");
        canvasGroup.blocksRaycasts = true;

        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.transform.tag == "BoardSlot")
            {
                if (hit.gameObject.transform.childCount < 3)
                {
                    if(hit.gameObject.GetComponent<ToeSpace>().propertyNeeded == itemTag)
                    {
                        //  the inventory slot is empty, we place here the item!
                        transform.SetParent(hit.gameObject.transform.GetChild(0).transform, true);
                        gameObject.GetComponent<Image>().raycastTarget = false;
                        itemHasBeenPlaced = true;
                        hit.gameObject.GetComponent<ToeSpace>().passSpaceNum();
                        canvasGroup.alpha = 1;
                    }
                    else
                    {
                        Debug.Log("Wrong Tag");
                    }

                }

            }
            else if(hit.gameObject.transform.tag == "SellZone")
            {
                //give player Money then
                activeInventory = GameObject.FindObjectOfType<InventoryMenu>().gameObject;
                activeInventory.GetComponent<InventoryMenu>().changeMoney(marketPrice);
                itemHasBeenPlaced = true;
                Destroy(gameObject);
            }
            else
            {
                if(itemHasBeenPlaced == false)
                {
                    transform.SetParent(parentAfterDrag, false);
                }

            }
            SellzoneRef.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("MouseDown");
    }
    public void OnDrop(PointerEventData eventData)
    {



    }
}
