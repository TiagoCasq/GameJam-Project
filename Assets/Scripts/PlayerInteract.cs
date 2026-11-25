using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private float Interact_Range = 50f;
    [SerializeField] private Inventory PlayerInventory;

    private InteractUI InteractionManager;

    private InputAction Action;

    private Transform CameraTransform;
    private Transform PlayerTransform;
    private Quaternion LastRotation;
    private Vector3 LastPosition;

    private Collider CurrentCollider = null;

    private float Held_For = 0f;

    private void ResetValues()
    {
        if (InteractionManager == null)
        {
            return;
        }
        CurrentCollider = null;
        Held_For = 0f;
        InteractionManager.UpdateProgress(0f);
    }

    private void Cast()
    {
        RaycastHit Hit;
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out Hit, Interact_Range)) 
        {
            if (Hit.collider.tag == "Interactible")
            {
                if (Hit.collider == CurrentCollider)
                {
                    return;
                }

                Interaction Interactible = Hit.collider.GetComponentInParent<Interaction>();

                if (Interactible != null)
                {
                    CurrentCollider = Hit.collider;

                    if (InteractionManager != null)
                    {
                        InteractionManager.DisplayText(Interactible.GetText());
                    }
                }

                Debug.Log(CurrentCollider);
            } 
            else
            {
                ResetValues();
                InteractionManager.DisplayText("");
            }
        }
        else
        {
            ResetValues();
            InteractionManager.DisplayText("");
        }
    }

    private void Awake()
    {
        CameraTransform = PlayerCamera.GetComponent<Transform>();
        PlayerTransform = PlayerCamera.GetComponentInParent<Transform>();
        Action = InputActions.FindActionMap("Player").FindAction("Interact");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractionManager = InteractUI.InteractionManager;
        //PlayerInventory.AddItem("Test Item");
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraTransform.rotation != LastRotation || PlayerTransform.position != LastPosition)
        {
            Cast();
        }

        LastRotation = CameraTransform.rotation;
        LastPosition = PlayerTransform.position;

        if (Action.IsPressed())
        {
            if (CurrentCollider)
            {
                Interaction Interactible = CurrentCollider.GetComponentInParent<Interaction>();

                if (Interactible != null)
                {
                    float HoldDuration = Interactible.GetDuration();

                    if (HoldDuration > 0)
                    {
                        if (Held_For >= HoldDuration)
                        {
                            InteractionManager.DisplayText("");
                            Interactible.Interact();
                            ResetValues();
                        }
                        else
                        {
                            Held_For += Time.deltaTime;
                            InteractionManager.UpdateProgress(Held_For/HoldDuration);
                        }
                    }
                    else
                    {
                        InteractionManager.DisplayText("");
                        Interactible.Interact();
                        ResetValues();
                    }
                }
            }
        }
        else
        {
            Held_For = 0f;
            InteractionManager.UpdateProgress(0f);
        }

        //Debug.Log(LastRotation);
    }
}
