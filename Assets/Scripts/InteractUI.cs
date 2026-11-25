using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    public static InteractUI InteractionManager { get; private set; }

    [SerializeField] private TextMeshProUGUI Interact_Text;
    [SerializeField] private Image Interact_Complete;
    [SerializeField] private float Max_Warn_Time = 3f;

    private float LastChange = 0f;

    private void Awake()
    {
        if (InteractionManager != null)
        {
            Destroy(this);
            return;
        }

        InteractionManager = this;
    }

    public void DisplayText(string ToText)
    {
        Interact_Text.text = ToText;
        LastChange = Time.realtimeSinceStartup;
    }

    public void UpdateProgress(float p)
    {
        Interact_Complete.fillAmount = p;
    }

    IEnumerator Warning_Count(float Change)
    {
        float Warn_Time = 0;

        while (Warn_Time < Max_Warn_Time)
        {
            Warn_Time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Interact_Text.text = (LastChange == Change) ? ("") : (Interact_Text.text);

    }
    public void Warn(string ToWarn)
    {
        Interact_Text.text = ToWarn;
        LastChange = Time.realtimeSinceStartup;

        StartCoroutine(Warning_Count(LastChange));
    }
}
