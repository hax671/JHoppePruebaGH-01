using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class XRJump : MonoBehaviour
{
    // Esta clase XRControls se genera automáticamente desde el InputActionAsset
    private XRControls controls;
    private CharacterController characterController;
    private Vector3 playerVelocity;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private bool groundedPlayer;

    private void Awake()
    {
        // Inicializa los controles
        controls = new XRControls();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // Activa el mapa de acciones y suscribe el evento de salto
        controls.Gameplay.Enable();
        controls.Gameplay.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        // Desuscribe para evitar errores
        controls.Gameplay.Jump.performed -= OnJump;
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        // Comprobar si está en el suelo
        groundedPlayer = characterController.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // mantiene al jugador pegado al suelo
        }

        // Aplicar gravedad
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (groundedPlayer)
        {
            // Fórmula para salto realista
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}

