using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private CameraManager cameraManagerRef;
    [SerializeField] private string nextLevel;
    public static int currentMaxWorld = 1;
    public static int currentMaxLevel = 1;
    [SerializeField] private int level;
    [SerializeField] private int world;


    private void Awake()
    {
        Time.timeScale = 1;
        player = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
        cameraManagerRef.playerTransform = player.transform;
        EventManager.Instance.GoNextLevel += LoadLevel;
    }
    private void Start()
    {
        if (world > currentMaxWorld)
        {
            currentMaxWorld = world;
            currentMaxLevel = level;
        }
        if (world == currentMaxWorld)
        {
            if (level > currentMaxLevel)
            {
                currentMaxLevel = level;
            }
        }
    }
    private void OnDestroy()
    {
        EventManager.Instance.GoNextLevel -= LoadLevel;
    }
    private void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
