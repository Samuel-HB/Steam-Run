using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private SceneAsset nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("in");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("out");
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
        SceneManager.LoadScene(nextLevel.name);
    }
}
