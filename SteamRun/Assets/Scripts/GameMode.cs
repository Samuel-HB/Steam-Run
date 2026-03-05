using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private SceneAsset nextLevel;


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
