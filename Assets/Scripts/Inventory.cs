using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "New Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] string[] Inventory_Items = new string[10];
    [SerializeField] int Max_Capacity = 10;

    public int GetSlotsFilled()
    {
        int LastPos = 0;

        foreach (var item in Inventory_Items)
        {
            if (item != "")
            {
                LastPos += 1;
            }
        }

        return LastPos;
    }

    public int GetCapacity()
    {
        return Max_Capacity;
    }

    public string[] GetInventory()
    {
        return Inventory_Items;
    }

    public void AddItem(string ToAdd)
    {
        int LastPos = 0;

        foreach (var item in Inventory_Items)
        {
            if (item != "")
            {
                LastPos += 1;
            }
        }

        if (LastPos < Max_Capacity)
        {
            Inventory_Items.SetValue(ToAdd, LastPos);
        }
    }

    public void RemoveItem(int ToRemove)
    {
        Inventory_Items.SetValue(null, ToRemove);

        Array.Sort(Inventory_Items);
    }
}
