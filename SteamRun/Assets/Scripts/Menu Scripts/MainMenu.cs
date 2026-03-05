using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
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
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
