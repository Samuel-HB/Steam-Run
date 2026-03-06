using UnityEngine;
using Unity.Cinemachine;

public class CameraCinemachine : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameMode gm;

    private void Start()
    {
        var Target = cam.Target;
        cam.Target.TrackingTarget = gm.player.transform;
    }
}
