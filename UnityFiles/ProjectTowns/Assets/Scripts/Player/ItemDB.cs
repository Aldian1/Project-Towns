using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {

    public Sprite[] sprites;

    public static List<Item> ItemList = new List<Item>();

    public List<Item> itemcopy = new List<Item>();

	// Use this for initialization
	void Awake () {
        

        #region  Item_Creation

        //Sword Item
        Item i0 = new Item();
        i0.name = "Sword";
        i0.type = Item.Type.Equip;
        i0.sprite_ = sprites[0];
        i0.stackable = Item.Stackable.Non_Stackable;
        ItemList.Add(i0);

        //Meat
        Item i1 = new Item();
        i1.name = "Meat";
        i1.type = Item.Type.Consumable;
        i1.sprite_ = sprites[1];
        i1.stackable = Item.Stackable.Stackable;
        i1.description = "Your Average Hunk Of Meat \n + 5 hunger";
        i1.maxstack = 40;
        ItemList.Add(i1);

        //potion

        Item i2 = new Item();
        i2.name = "Potion";
        i2.type = Item.Type.Consumable;
        i2.sprite_ = sprites[2];
        i2.stackable = Item.Stackable.Stackable;
        i2.description = "Rejuvination to the max";
        i2.maxstack = 15;
        ItemList.Add(i2);

        //stone
        Item i3 = new Item();
        i3.name = "Stone";
        i3.type = Item.Type.Resource;
        i3.sprite_ = sprites[3];
        i3.stackable = Item.Stackable.Stackable;
        i3.description = "A dull grey, cold and ragged";
        i3.maxstack = 80;
        ItemList.Add(i3);

        //wood
        Item i4 = new Item();
        i4.name = "Wood";
        i4.type = Item.Type.Resource;
        i4.sprite_ = sprites[4];
        i4.stackable = Item.Stackable.Stackable;
        i4.description = "Looks like its craftable";
        i4.maxstack = 80;
        ItemList.Add(i4);

        //plank
        Item i5 = new Item();
        i5.name = "Plank";
        i5.type = Item.Type.Resource;
        i5.sprite_ = sprites[5];
        i5.stackable = Item.Stackable.Stackable;
        i5.description = "Looks straight and authorative";
        i5.maxstack = 26;
        ItemList.Add(i5);

        //Fish
        Item i6 = new Item();
        i6.name = "Fish";
        i6.type = Item.Type.Consumable;
        i6.sprite_ = sprites[6];
        i6.stackable = Item.Stackable.Stackable;
        i6.description = "Slimy and smelly";
        i6.maxstack = 26;
        ItemList.Add(i6);


        //
        Item i7 = new Item();
   
        #endregion
        itemcopy = ItemList;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
