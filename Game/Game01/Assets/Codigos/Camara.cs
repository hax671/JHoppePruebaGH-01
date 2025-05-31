using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform target;          // La esfera (player)
    public Vector3 offset = new Vector3(0, 5, -10); // Posición relativa
    public float smoothSpeed = 0.125f;              // Suavidad del seguimiento

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(target); // Opcional: que mire siempre al objetivo
    }
}

