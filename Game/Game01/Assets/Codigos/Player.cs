using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Velocidad de Movimiento")]
    public float forwardSpeed = 10f;
    public float backwardSpeed = 5f;
    public float leftSpeed = 7f;
    public float rightSpeed = 7f;

    [Header("Configuración de Movimiento")]
    public bool useTorque = true;

    [Header("Empuje al Enemigo")]
    public float pushForce = 500f;

    [Header("Color al Colisionar")]
    public float colorResetDelay = 0.5f;

    [Header("Texto flotante de daño")]
    public GameObject floatingTextPrefab; // Prefab de texto 3D con texto ya configurado
    public Vector3 textOffset = new Vector3(0, 2f, 0); // Posición relativa al enemigo

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
                // Empuje físico
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                otherRb.AddForce((direction + Vector3.up) * pushForce);

                // Cambio temporal de color
                Renderer rend = collision.gameObject.GetComponent<Renderer>();
                if (rend != null)
                {
                    StartCoroutine(ChangeColorTemporarily(rend, Color.red, colorResetDelay));
                }

                // Mostrar texto flotante encima del enemigo (usa texto del prefab)
                if (floatingTextPrefab != null)
                {
                    Vector3 spawnPos = collision.transform.position + textOffset;
                    GameObject textObj = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);
                    // NO cambiamos el texto, lo toma tal cual del prefab
                    Destroy(textObj, 0.5f);
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












