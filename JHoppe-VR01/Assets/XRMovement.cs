using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class XRMovement : MonoBehaviour
{
    private XRControls controls;
    private CharacterController characterController;
    private Vector3 velocity;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashDuration = 0.2f; // tiempo que dura el impulso
    [SerializeField] private float dashCooldown = 1f;

    private bool canDash = true;
    private bool isDashing = false;
    private Vector3 dashStart;
    private Vector3 dashEnd;
    private float dashTime;

    private void Awake()
    {
        controls = new XRControls();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Jump.performed += OnJump;
        controls.Gameplay.Dash.performed += OnDash;
    }

    private void OnDisable()
    {
        controls.Gameplay.Jump.performed -= OnJump;
        controls.Gameplay.Dash.performed -= OnDash;
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        if (isDashing)
        {
            dashTime += Time.deltaTime;
            float t = dashTime / dashDuration;

            if (t >= 1f)
            {
                isDashing = false;
                return;
            }

            Vector3 newPos = Vector3.Lerp(dashStart, dashEnd, t);
            Vector3 move = newPos - transform.position;
            characterController.Move(move);
        }
        else
        {
            // Movimiento vertical con gravedad
            if (characterController.isGrounded && velocity.y < 0)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded && !isDashing)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (!canDash || isDashing) return;

        // Dirección hacia donde mira la cámara (solo horizontal)
        Vector3 dashDirection = Camera.main.transform.forward;
        dashDirection.y = 0;
        dashDirection.Normalize();

        dashStart = transform.position;
        dashEnd = dashStart + dashDirection * dashDistance;
        dashTime = 0f;
        isDashing = true;

        StartCoroutine(DashCooldown());
    }

    private System.Collections.IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

