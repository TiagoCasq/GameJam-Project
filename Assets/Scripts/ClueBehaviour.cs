using UnityEngine;

public class ClueBehaviour : MonoBehaviour, Interaction
{
    [SerializeField] private string Object_Text;
    [SerializeField] private float Hold_Duration = 0f;
    [SerializeField] private string Clue_Name;
    [SerializeField] private string Clue_Description;

    private MainInput InputHandler;

    public void Start()
    {
        InputHandler = MainInput.InputHandler;
    }

    public void Interact()
    {
        if (InputHandler != null)
        {
            InputHandler.WriteClue(Clue_Name, Clue_Description, true);
            Destroy(gameObject); 
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
}
