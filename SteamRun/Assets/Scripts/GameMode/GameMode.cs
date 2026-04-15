using UnityEngine;

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
    [SerializeField] private GameObject containerVictory;
    public static bool isGamePaused = false;

    private void Awake()
    {
        EventManager.Instance.EndLevelReached += ShowVictoryContainer;

        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
        cameraManagerRef.playerTransform = player.transform;
    }

    private void Start()
    {
        containerVictory.SetActive(false);
    }

    private void ShowVictoryContainer()
    {
        Time.timeScale = 0;
        containerVictory.SetActive(true);
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDestroy()
    {
        EventManager.Instance.EndLevelReached -= ShowVictoryContainer;
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
