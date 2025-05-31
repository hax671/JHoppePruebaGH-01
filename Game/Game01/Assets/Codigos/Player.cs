using UnityEngine;
using System.Collections; // <- Necesario para corrutinas

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public float forwardSpeed = 10f;
    public float backwardSpeed = 5f;
    public float leftSpeed = 7f;
    public float rightSpeed = 7f;

    public bool useTorque = true;
    public float pushForce = 500f;
    public float colorResetDelay = 0.5f; // Tiempo para volver al color original

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            useTorque = !useTorque;
            rb.freezeRotation = !useTorque;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody otherRb = collision.rigidbody;

            if (otherRb != null && otherRb != rb)
            {
                // Aplicar fuerza de empuje
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                otherRb.AddForce((direction + Vector3.up) * pushForce);

                // Cambiar color temporalmente
                Renderer rend = collision.gameObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    StartCoroutine(ChangeColorTemporarily(rend, Color.red, colorResetDelay));
                }
            }
        }
    }

    IEnumerator ChangeColorTemporarily(Renderer rend, Color hitColor, float delay)
    {
        Color originalColor = rend.material.color;
        rend.material.color = hitColor;
        yield return new WaitForSeconds(delay);
        rend.material.color = originalColor;
    }
}








