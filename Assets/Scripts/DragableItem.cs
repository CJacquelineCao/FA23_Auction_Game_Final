using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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
    public string itemName;
    public string itemUtil;
    public int marketPrice;

    public GameObject activeInventory;

    public GameObject ItemDescription;
    public GameObject ActualDesc;
    GameController tictac;
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _transform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        SellzoneRef = GameObject.FindGameObjectWithTag("SellZone");
        tictac = FindObjectOfType<GameController>();
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
        ItemDescription.SetActive(false);
        _transform.position += (Vector3)eventData.delta * canvas.transform.localScale.x / canvas.scaleFactor;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stopped Drag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
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
        ItemDescription = tictac.Description;
        if(ItemDescription !=null)
        {
            ItemDescription.SetActive(true);
            ItemDescription.GetComponent<DescriptionHolder>().currentActiveObject = this.gameObject.GetComponent<DragableItem>();
            ActualDesc = ItemDescription.transform.GetChild(0).gameObject;
            Vector3 Itempos = new Vector3(gameObject.GetComponent<RectTransform>().position.x -1 , gameObject.GetComponent<RectTransform>().position.y, 0);
            ActualDesc.transform.position = Itempos;
            ActualDesc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
            ActualDesc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemTag;
            ActualDesc.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemUtil;
        }
        Debug.Log("MouseDown");





    }
    public void OnDrop(PointerEventData eventData)
    {



    }
}
