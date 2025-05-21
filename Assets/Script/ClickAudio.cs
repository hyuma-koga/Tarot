using UnityEngine;

public class ClickAudio : MonoBehaviour
{
    public AudioClip clickSE;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (clickSE != null)
        {
            audioSource.PlayOneShot(clickSE);
        }
    }
}
