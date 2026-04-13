using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    [SerializeField] private string nextLevel;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(nextLevel);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        EventManager.Instance.PlayerDeathFunc();
    }
}
