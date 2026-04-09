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
        EventManager.Instance.Interact += GoToLevel;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Interact -= GoToLevel;
    }

    void GoToLevel()
    {
        if (nextLevel != null && playerInFront == true)
        {
            SceneManager.LoadScene(nextLevel.name);
        }       
    }
}
