using UnityEngine;
using UnityEngine.InputSystem;

public class XRShotController : MonoBehaviour
{
    public Animator animator;              // Asigna tu Animator
    public InputActionReference shootActionReference; // Asigna aquí tu acción Shoot

    private void OnEnable()
    {
        // Suscribirse al evento performed
        shootActionReference.action.performed += OnShootPerformed;
        shootActionReference.action.Enable();
    }

    private void OnDisable()
    {
        // Limpiar suscripción al deshabilitar
        shootActionReference.action.performed -= OnShootPerformed;
        shootActionReference.action.Disable();
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Shoot");  // Dispara la animación
    }
}
