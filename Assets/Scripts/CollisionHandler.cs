using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Finish":
                Debug.Log("Collided with Finish object.");
                break;
            case "Fuel":
                Debug.Log("Collided with Fuel object.");
                break;
            default:
                ReloadLevel();
                break;
        }       
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
