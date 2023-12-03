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

        tictacRef.checkToeSpaces();
    }
    void WeaponAbility()
    {

        if (tictacRef.whosTurn == 1)
        {
            manageref.player2Cursed = true;
        }
        else if(tictacRef.whosTurn == 2)
        {
            manageref.player1Cursed = true;
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
