using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;

    // Velocidades personalizadas por dirección
    public float forwardSpeed = 10f;
    public float backwardSpeed = 5f;
    public float leftSpeed = 7f;
    public float rightSpeed = 7f;

    // Alternar entre modo rodado (físico) y directo
    public bool useTorque = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false; // Permitir rotación por defecto
    }

    void Update()
    {
        // Cambiar modo con la tecla T
        if (Input.GetKeyDown(KeyCode.T))
        {
            useTorque = !useTorque;
            rb.freezeRotation = !useTorque; // Evita que rote si no se usa torque
            Debug.Log("Modo: " + (useTorque ? "Rodando (físico)" : "Movimiento directo"));
        }
    }

    void FixedUpdate()
    {
        if (useTorque)
        {
            ApplyTorqueMovement();
        }
        else
        {
            ApplyDirectMovement();
        }
    }

    void ApplyTorqueMovement()
    {
        Vector3 torque = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) torque += Vector3.right * forwardSpeed;
        if (Input.GetKey(KeyCode.S)) torque += Vector3.left * backwardSpeed;
        if (Input.GetKey(KeyCode.A)) torque += Vector3.forward * leftSpeed;
        if (Input.GetKey(KeyCode.D)) torque += Vector3.back * rightSpeed;

        rb.AddTorque(torque);
    }

    void ApplyDirectMovement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.forward * forwardSpeed;
        if (Input.GetKey(KeyCode.S)) move += Vector3.back * backwardSpeed;
        if (Input.GetKey(KeyCode.A)) move += Vector3.left * leftSpeed;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right * rightSpeed;

        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}





