using UnityEngine;

public class RobotControlador : MonoBehaviour
{
    public Animator animator;

    public void Awake()
    {
        animator.SetBool("Reproducir", false); // Asegura que empieza detenido
    }

    public void OnMouseDown()
    {
         if (animator == null) return;

        // Alternar el estado de "Reproducir"
        bool actual = animator.GetBool("Reproducir");
        animator.SetBool("Reproducir", !actual);
    }
}
