using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private SceneAsset nextLevel;

    private void Awake()
    {
        EventManager.Instance.EndLevelReached += LoadNewLevel;

        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
    }

    private void LoadNewLevel()
    {
        SceneManager.LoadScene(nextLevel.name);
    }

    private void OnDestroy()
    {
        EventManager.Instance.EndLevelReached -= LoadNewLevel;
    }
}
