using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.0f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        isControllable = false;
    }

    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene % SceneManager.sceneCountInBuildSettings);
    }

    void StartSuccessSequence()
    {
        DisableMovement();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void StartCrashSequence()
    {
        DisableMovement();
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", levelLoadDelay);
    }
}
