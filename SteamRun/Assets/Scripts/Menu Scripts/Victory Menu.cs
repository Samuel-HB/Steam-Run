using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject containerVictory;
    public static bool isGameVictoryOn = false;
    [SerializeField] private TMP_Text timerText;
    void Start()
    {
        EventManager.Instance.EndLevelReached += SetVictoryMenu;
        containerVictory.SetActive(false);
    }
    private void SetVictoryMenu()
    {
        isGameVictoryOn = true;
        Time.timeScale = 0;
        containerVictory.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void NextLevel()
    {
        isGameVictoryOn = false;
        EventManager.Instance.NextLevelFunc();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        containerVictory.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventManager.Instance.PlayerDeathFunc();
        isGameVictoryOn = false;
    }
    public void MainMenu()
    {
        isGameVictoryOn = false;
        SceneManager.LoadScene("Worlds Selection");
    }
    public void Levels()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isGameVictoryOn = false;
        SceneManager.LoadScene("Worlds Selection");
    }
    public void SetTimer(int hour, int minute , int second)
    {
        timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
    }
}
