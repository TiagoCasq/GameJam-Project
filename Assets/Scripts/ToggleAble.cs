using UnityEngine;

public class ToggleAble : MonoBehaviour, Interaction
{
    [SerializeField] private string Object_Text;
    [SerializeField] private float Hold_Duration;
    [SerializeField] private Material Toggled_Material;

    private Renderer Object_Rederer;
    private bool Toggled = false;

    private void Awake()
    {
        Object_Rederer = GetComponent<Renderer>();
    }
    public void Interact()
    {
        if (Toggled)
        {
            return;
        }

        if (Object_Rederer != null)
        {
            Object_Rederer.material = Toggled_Material;
        }

        tag = "Untagged";

        Debug.Log("Toggled "+Object_Text);
    }

    public string GetText() {
        return Object_Text;
    }

    public float GetDuration()
    {
        return Hold_Duration;
    }
}
