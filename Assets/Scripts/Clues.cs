using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Clues", menuName = "New Clue List")]

public class Clues : ScriptableObject
{
    [SerializeField] private Dictionary<string, string> Clue_List = new();
    [SerializeField] private List<string> Clue_Names = new();
    [SerializeField] private List<string> Clue_Description = new();

    public void Clear()
    {
        Clue_Names.Clear();
        Clue_Description.Clear();
    }

    public void AddClue(string NewClue, string NewDescription)
    {
        Clue_Names.Add(NewClue);
        Clue_Description.Add(NewDescription);
    }

    public Dictionary<int, string[]> GetClues()
    {
        Dictionary<int, string[]> Result = new();

        for (int i = 0; i < Clue_Names.Count; i++)
        {
            string[] merge = new string[2];
            merge[0] = Clue_Names[i];
            merge[1] = Clue_Description[i];

            Result.Add(i, merge);
        }

        return Result;
    }
}
