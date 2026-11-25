using UnityEngine;

public class Collectible : MonoBehaviour, Interaction
{
    [SerializeField] private string Object_Text;
    [SerializeField] private string Inventory_Name;
    [SerializeField] private float Hold_Duration = 0f;
    [SerializeField] private Inventory PlayerInventory;

    private InteractUI InteractionManager;

    public void Interact()
    {
        if (PlayerInventory != null)
        {
            int Capacity = PlayerInventory.GetCapacity();
            int Filled = PlayerInventory.GetSlotsFilled();

            if (Filled < Capacity)
            {
                PlayerInventory.AddItem(Inventory_Name);
                Destroy(gameObject);
                Debug.Log("Collected " + Object_Text);
            }
            else if (InteractionManager != null)
            {
                InteractionManager.Warn("Inventory is full!");
            }
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Collected " + Object_Text);
        }
    }

    public string GetText() {
        return Object_Text;
    }

    public float GetDuration()
    {
        return Hold_Duration;
    }

    public void Start()
    {
        InteractionManager = InteractUI.InteractionManager;
    }
}
