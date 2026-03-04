using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;

    [SerializeField] private SceneAsset nextLevel;
    [SerializeField] private Transform endLevelPoint;


    private void Start()
    {
        EventManager.Instance.RestartLevel += RestartLevel;
        EventManager.Instance.EndLevelReached += LoadNewLevel;

        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // quand joueur mort ou niveau stoppé
        {
            EventManager.Instance.PlayerDeathFunc();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            EventManager.Instance.StartLevelFunc();
        }
    }

    private void RestartLevel()
    {
        player.transform.position = startPosition.position;
    }

    private void LoadNewLevel()
    {
        SceneManager.LoadScene(nextLevel.name);
    }
}
