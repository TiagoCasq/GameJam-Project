using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour, Interaction
{

    [Header("General")]
    [SerializeField] private string Object_Text;
    [SerializeField] private float Hold_Duration;
    [SerializeField] private float OpenAngle;
    [SerializeField] private float AnimDuration;

    [Header("Locked Doors")]
    [SerializeField] private bool Locked = false;
    [SerializeField] private string Required_Object = "Empty";
    [SerializeField] private Inventory PlayerInventory;

    private bool Open = false;
    private bool Animating = false;
    private InteractUI InteractionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InteractionManager = InteractUI.InteractionManager;
    }

    IEnumerator DoorAnim()
    {

        float AngleIncremental = OpenAngle/AnimDuration;
        float Step = 0;

        float current_angle = transform.rotation.eulerAngles.y;
        float GoalAngle = (!Open) ? current_angle + OpenAngle : current_angle - OpenAngle;

        while ( (AngleIncremental < 0 && Step > OpenAngle) || (AngleIncremental >= 0 && Step < OpenAngle))
        {
            Step += (AngleIncremental) * Time.deltaTime;
            transform.rotation = (!Open) ? Quaternion.Euler(0, current_angle + Step, 0) : Quaternion.Euler(0, current_angle - Step, 0);
            //Debug.Log(OpenAngle);
            //Debug.Log(Step);
            //Debug.Log(Step > OpenAngle);
            yield return new WaitForFixedUpdate();
        }

        transform.rotation = Quaternion.Euler(0, GoalAngle, 0);
        Open = !Open;
        Animating = false;

    }
    

    public void Interact()
    {
        if (Locked && PlayerInventory != null)
        {
            string[] current_inventory = PlayerInventory.GetInventory();
            int Index = 0;

            foreach (var item in current_inventory)
            {
                if (item == Required_Object)
                {
                    break;
                }

                Index++;
            }

            if (Index < current_inventory.Length)
            {
                PlayerInventory.RemoveItem(Index);
                Locked = false;
            } 
            else
            {
                if (InteractionManager != null)
                {
                    InteractionManager.Warn("This door is locked.");
                }
                return;
            }
        }

        if (!Animating)
        {
            Animating = true;
            StartCoroutine(DoorAnim());
        }

        return;
    }

    public string GetText()
    {
        return Object_Text;
    }

    public float GetDuration()
    {
        return Hold_Duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
