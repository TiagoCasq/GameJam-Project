using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Double_Doors : MonoBehaviour, Interaction
{

    [SerializeField] private string Object_Text;
    [SerializeField] private float Hold_Duration;
    [SerializeField] private float OpenAngle;
    [SerializeField] private float AnimDuration;
    [SerializeField] private Transform Right_Door;
    [SerializeField] private Transform Left_Door;

    private bool Open = false;
    private bool Animating = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    IEnumerator DoorAnim()
    {

        float AngleIncremental = OpenAngle/AnimDuration;
        float Step = 0;

        float current_angle = Right_Door.rotation.eulerAngles.y;
        float lCurrent_angle = Left_Door.rotation.eulerAngles.y;
        float GoalAngle = (!Open) ? current_angle + OpenAngle : current_angle - OpenAngle;
        float LGoalAngle = (!Open) ? lCurrent_angle - OpenAngle : lCurrent_angle + OpenAngle;

        while (Step < OpenAngle)
        {
            Step += (math.abs(AngleIncremental)) * Time.deltaTime;
            Right_Door.rotation = (!Open) ? Quaternion.Euler(0, current_angle + Step, 0) : Quaternion.Euler(0, current_angle - Step, 0);
            Left_Door.rotation = (!Open) ? Quaternion.Euler(0, lCurrent_angle - Step, 0) : Quaternion.Euler(0, lCurrent_angle + Step, 0);
            Debug.Log(Step);
            yield return new WaitForFixedUpdate();
        }

        Right_Door.rotation = Quaternion.Euler(0, GoalAngle, 0);
        Left_Door.rotation = Quaternion.Euler(0, LGoalAngle, 0);
        Open = !Open;
        Animating = false;

    }
    

    public void Interact()
    {
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
