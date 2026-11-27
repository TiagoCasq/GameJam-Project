using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainInput : MonoBehaviour
{
    [SerializeField] private GameObject Book;
    [SerializeField] private TextMeshProUGUI TextTemplate;
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private Clues ClueSO;

    [SerializeField] private GameObject[] Pages = new GameObject[6];
    [SerializeField] private int MaxPairs = 3;

    public static MainInput InputHandler { get; private set; }

    private InputAction Book_Trigger;

    private bool Book_Equipped = false;
    private int Current_Clues = 0;
    private int Current_Page = 1;
    private int Current_Pair = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (MainInput.InputHandler != null)
        {
            Destroy(this);
            return;
        }

        MainInput.InputHandler = this;

        Book_Trigger = InputActions.FindAction("OpenClues");
    }
    void Start()
    {
        if (ClueSO)
        {
            Dictionary<int, string[]> ClueDictionary = ClueSO.GetClues();

            foreach (var item in ClueDictionary)
            {
                WriteClue(item.Value[0], item.Value[1], false);
            }
        }
    }

    private void CyclePages(bool truefalse)
    {
        Pages[Current_Pair * 2 - 1].SetActive(truefalse);
        Pages[Current_Pair * 2 - 2].SetActive(truefalse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Book_Trigger.WasPressedThisFrame())
        {
            Book.SetActive(!Book_Equipped);
            Book_Equipped = !Book_Equipped;
        }

        if (Book_Equipped)
        {
            if (InputActions.FindAction("SecondaryAction").WasPressedThisFrame())
            {
                if (Current_Pair < MaxPairs)
                {
                    CyclePages(false);

                    Current_Pair++;

                    CyclePages(true);
                }
            }
            else if (InputActions.FindAction("MainAction").WasPressedThisFrame())
            {
                if (Current_Pair > 1)
                {
                    CyclePages(false);

                    Current_Pair--;

                    CyclePages(true);
                }
            }
        }
    }

    //Book Visuals
    public void WriteClue(string NewClue, string NewDescription, bool Save)
    {
        if (Current_Clues >= 12)
        {
            return;
        }

        GameObject NewHolder = Instantiate(TextTemplate.gameObject);

        TextMeshProUGUI NewText = NewHolder.GetComponent<TextMeshProUGUI>();
        NewText.text = NewClue + ": " + NewDescription;
        NewText.name = "NewClue";

        NewHolder.SetActive(true);

        NewHolder.GetComponent<RectTransform>().SetParent(Pages[Current_Page].GetComponent<Transform>(), false);

        Current_Clues++;

        Current_Page = (Current_Clues % 4 == 0) ? Current_Page + 1 : Current_Page;

        if (Save)
        {
            ClueSO.AddClue(NewClue, NewDescription);
        }
    }
}
