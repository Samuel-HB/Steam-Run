using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private SceneAsset nextLevel;

    bool playerInFront =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("in");
        playerInFront = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("out");
        playerInFront =false;
    }

    private void Start()
    {
        SC_InputManager.instance.onInteractButtonPress += GoToLevel;
    }

    private void OnDestroy()
    {
        SC_InputManager.instance.onInteractButtonPress -= GoToLevel;
    }

    void GoToLevel()
    {
        if (nextLevel != null && playerInFront == true)
        {
            SceneManager.LoadScene(nextLevel.name);
        }       
    }
}
