using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] float FOV = 90;

    public void SetFOV(float value)
    {
        FOV = value;
    }

    public float GetFOVValue()
    {
        return FOV;
    }


}
