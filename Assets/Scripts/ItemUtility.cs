using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtility : MonoBehaviour
{
    TicTacToeController tictacRef;
    public DragableItem itemref;
    AllBidItemsManager manageref;


    public bool Used;
    // Start is called before the first frame update
    void Start()
    {
        tictacRef = GameObject.FindObjectOfType<TicTacToeController>();
        manageref = GameObject.FindObjectOfType<AllBidItemsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem()
    {
        if(Used == false)
        {

            itemref.ItemDescription.SetActive(false);
            if (itemref.itemTag == "Tool")
            {

                ToolAbility();
            }
            else if (itemref.itemTag == "Weapon")
            {

                WeaponAbility();
            }
            else if (itemref.itemTag == "Magical")
            {

                MagicAbility();
            }
            Used = true;
        }

        Destroy(this.gameObject);
    }
    void ToolAbility()
    {
        if(itemref.itemName == "Hammer")
        {
            tictacRef.checkToeSpaces();

        }
        else if(itemref.itemName == "Quill")
        {
            tictacRef.CheckPossession();
        }
        else if(itemref.itemName == "Lockpick")
        {
            if(tictacRef.whosTurn == 1)
            {
                tictacRef.P2Inventory.transform.GetChild(0).GetComponent<InventoryMenu>().changeMoney(-50);
                tictacRef.P1Inventory.transform.GetChild(0).GetComponent<InventoryMenu>().changeMoney(50);
            }
            else if(tictacRef.whosTurn == 2)
            {
                tictacRef.P1Inventory.transform.GetChild(0).GetComponent<InventoryMenu>().changeMoney(-50);
                tictacRef.P2Inventory.transform.GetChild(0).GetComponent<InventoryMenu>().changeMoney(50);
            }
        }

    }
    void WeaponAbility()
    {
        if(itemref.itemName == "Dagger")
        {
            if (tictacRef.whosTurn == 1)
            {
                manageref.player2Cursed = true;
            }
            else if (tictacRef.whosTurn == 2)
            {
                manageref.player1Cursed = true;
            }
        }
        else if(itemref.itemName == "Crossbow")
        {
            if(tictacRef.whosTurn == 1)
            {
                manageref.player1canshoot = true;
            }
            else if(tictacRef.whosTurn == 2)
            {
                manageref.player2canshoot = true;
            }
        }

    }
    void MagicAbility()
    {

        if (tictacRef.whosTurn == 1)
        {
            manageref.player1Drank = true;
        }
        else if (tictacRef.whosTurn == 2)
        {
            manageref.player2Drank = true;
        }
    }
}
