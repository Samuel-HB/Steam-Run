using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private int world;
    [SerializeField] private int level;
    private bool isOpen =false;


    bool playerInFront =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("in");
            playerInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("out");
            playerInFront = false;
        }
    }

    private void Start()
    {
        EventManager.Instance.Interact += GoToLevel;
        if (GameMode.currentWorld >= world && GameMode.currentLevel >= level)
        {
            isOpen = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.Interact -= GoToLevel;
    }

    void GoToLevel()
    {
        if (nextLevel != null && playerInFront == true && isOpen ==true)
        {
            SceneManager.LoadScene(nextLevel);
        }       
    }
}
