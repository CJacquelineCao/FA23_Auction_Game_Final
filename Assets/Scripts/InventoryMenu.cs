using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryMenu : MonoBehaviour
{
    [System.Serializable]
    public class MenuItem
    {
        public GameObject MenuSlot;
        public DragableItem item;
        public bool filled;
    }
    public List<MenuItem> allItems = new List<MenuItem>();
    public GameObject ItemPrefab;

    public int currentMoneyCount;
    public TMPro.TextMeshProUGUI moneyDisplay;
    private Canvas _canvas;


    // Start is called before the first frame update
    void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

    }

    public void changeMoney(int money)
    {
        currentMoneyCount += money;
    }
     void AssignSlot()
    {
        for(int i =0; i<allItems.Count;i++)
        {
            if(allItems[i].item != null)
            {
                allItems[i].item.parentAfterDrag = allItems[i].MenuSlot.transform;
                if(allItems[i].item.itemHasBeenPlaced == true)
                {
                    allItems[i].item = null;

                }
            }
        }
    }

    void checkMenuStatus()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].item != null)
            {
                allItems[i].filled = true;
            }
            else
            {
                allItems[i].filled = false;
            }
        }
    }

    public void addItems(DragableItem itemtoAdd)
    {
        bool isdone = false;
        bool finishedchecking = false;
        int i;
        while (finishedchecking == false)
        {
            for (i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].item != null)
                {

                }
                else
                {
                    //there's nothing in this slot
                    if (isdone == true)
                    {
                        //all slots have been checked
                        allItems[i].item = itemtoAdd;
                        allItems[i].filled = true;
                        allItems[i].item.transform.SetParent(allItems[i].MenuSlot.transform, true);
                        isdone = false;
                        finishedchecking = true;
                        return;
                    }
                    else
                    {
                        //let's move on to the next slot
                        continue;
                    }

                }

            }
            isdone = true; //ive checked all the slots, theres nothing.
            i = 0;
        }

    }
    // Update is called once per frame
    void Update()
    {
        checkMenuStatus();
        AssignSlot();
        moneyDisplay.text = "Money: " + currentMoneyCount + " shillings";
    }

}
