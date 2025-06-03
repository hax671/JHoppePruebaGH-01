using UnityEngine;

public class HuntControlador : MonoBehaviour
{
    public Animator animator;

    // Duraci�n estimada de la animaci�n (ajusta este valor)
    public float duracionAnimacion = 2.5f;

    private void Awake()
    {
        if (animator != null)
        {
            animator.SetBool("Reproducir", false); // Asegura que el robot empiece en Idle
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cubo"))
        {
            if (animator != null && animator.GetBool("Reproducir") == false)
            {
                animator.SetBool("Reproducir", true); // Activa la animaci�n
                Invoke(nameof(VolverAIdle), duracionAnimacion); // Espera y vuelve a Idle
            }
        }
    }

    private void VolverAIdle()
    {
        if (animator != null)
        {
            animator.SetBool("Reproducir", false); // Regresa al estado Idle
        }
    }
}
