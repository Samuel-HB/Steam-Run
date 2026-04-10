using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    // Scene Asset will prevent build game so use of string for load levels

    //[SerializeField] private string levelName;
    [SerializeField] private SceneAsset nextLevel;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(nextLevel.name);
        //SceneManager.LoadScene(levelName);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
