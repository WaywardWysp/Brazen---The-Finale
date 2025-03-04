using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioClip clickSound; // The audio clip to play
    public string sceneToLoad;  // Name of the scene to load
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio
        audioSource.playOnAwake = false;
        audioSource.clip = clickSound;
    }

    void OnMouseDown()
    {
        // Play audio cue
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        // Wait
        yield return new WaitForSeconds(1f);

        // Load scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

