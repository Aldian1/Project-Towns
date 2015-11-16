using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Items : MonoBehaviour
{

    public static string Name;
    public static int Amount;

    public class items
    {

        public void ItemData(string name, int amount)
        {

           Name = name;
            Amount = amount;
            
        }

    }
	
}
