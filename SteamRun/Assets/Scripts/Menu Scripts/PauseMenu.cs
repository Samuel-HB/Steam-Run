using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject containerPause;
    private bool isGamePaused = false;
    [SerializeField] private string mainMenu;
    [SerializeField] private string worldSelection;

    private void Start()
    {
        containerPause.SetActive(false);
        EventManager.Instance.PauseGame += PauseHasBeenUsed;
    }
    private void PauseHasBeenUsed()
    {
        if (VictoryMenu.isGameVictoryOn == false)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else 
            {
                PausedGame();
            }
        }
    }
    public void PausedGame()
    {
        Time.timeScale = 0;
        containerPause.SetActive(true);
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        containerPause.SetActive(false);
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu() // répétition avec le start du mainMenu (tout dupliquer par sécurité ?)
    {
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(mainMenu);
    }
    public void RestartLevel()
    {
        EventManager.Instance.PlayerDeathFunc();
        ResumeGame();
    }
    public void GoToWorldSelection()
    {
        ResumeGame();
        SceneManager.LoadScene(worldSelection);
    }

}
