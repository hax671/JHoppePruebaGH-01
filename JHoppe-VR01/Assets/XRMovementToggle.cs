using UnityEngine;
using UnityEngine.InputSystem;

public class XRMovementToggle : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private XRMovement movementScript; 

    [Header("Input Actions Asset")]
    [SerializeField] private InputActionAsset inputActions; 

    private InputAction toggleAction;

    private void OnEnable()
    {
        
        toggleAction = inputActions.FindAction("Gameplay/ToggleMovement", true);
        toggleAction.performed += OnToggle;
        toggleAction.Enable();
    }

    private void OnDisable()
    {
        toggleAction.performed -= OnToggle;
        toggleAction.Disable();
    }

    private void OnToggle(InputAction.CallbackContext context)
    {
        movementScript.enabled = !movementScript.enabled;
        Debug.Log("XRMovement ahora está: " + (movementScript.enabled ? "Activo ✅" : "Desactivado ❌"));
    }
}



