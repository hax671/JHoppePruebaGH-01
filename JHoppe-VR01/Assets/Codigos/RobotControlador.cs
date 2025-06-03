using UnityEngine;

public class RobotControlador : MonoBehaviour
{
    public Animator animator;

    public void Awake()
    {
        animator.SetBool("Reproducir", false); // Asegura que empieza detenido
    }

    public void BotonEncendido()
    {
         if (animator == null) return;

        // Alterna el estado de "Reproducir"
        bool actual = animator.GetBool("Reproducir");
        animator.SetBool("Reproducir", !actual);
    }
}
