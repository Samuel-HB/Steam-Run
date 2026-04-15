using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [HideInInspector ] public GameObject player;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private CameraManager cameraManagerRef;
    [SerializeField] private string nextLevel;
    public static int currentWorld = 1;
    public static int currentLevel = 1;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print(currentWorld);
            print("hdede");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentLevel += 1;
        }
    }
}
