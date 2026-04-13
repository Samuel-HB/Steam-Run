using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private CameraManager cameraManagerRef;
    [SerializeField] private string nextLevel;

    private void Awake()
    {
        EventManager.Instance.EndLevelReached += LoadNewLevel;

        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
        cameraManagerRef.playerTransform = player.transform;
    }

    private void LoadNewLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void OnDestroy()
    {
        EventManager.Instance.EndLevelReached -= LoadNewLevel;
    }
}
