using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "New Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private string[] Inventory_Items = new string[10];
    [SerializeField] private int Max_Capacity = 10;

    public int GetSlotsFilled()
    {
        int LastPos = 0;

        foreach (var item in Inventory_Items)
        {
            if (item != "")
            {
                LastPos += 1;
            }
            else
            {
                break;
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

    private void Sort()
    {
        List<int> EmptySlots = new List<int>();
        int index = 0;

        foreach (var item in Inventory_Items)
        {
            if (item == "")
            {
                EmptySlots.Add(index);
            }
            index++;
        }

        index = 0;

        foreach (var item in Inventory_Items)
        {
            if (item != "" && index > EmptySlots[0])
            {
                Inventory_Items[EmptySlots[0]] = item;
                Inventory_Items[index] = "";
                EmptySlots.RemoveAt(0);
                EmptySlots.Add(index);
                EmptySlots.Sort();
            }

            index++;
        }
    }

    public void RemoveItem(int ToRemove)
    {
        Inventory_Items.SetValue("", ToRemove);
        Sort();
    }
}
