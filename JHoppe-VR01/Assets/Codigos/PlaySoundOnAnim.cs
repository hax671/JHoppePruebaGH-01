using UnityEngine;

public class PlaySoundOnAnim : MonoBehaviour
{
    public AudioSource audioSource; // Asignar desde el inspector

    public void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}

