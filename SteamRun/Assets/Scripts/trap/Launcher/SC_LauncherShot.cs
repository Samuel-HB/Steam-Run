using System.Collections;
using UnityEngine;

public class SC_LauncherShot : MonoBehaviour
{
    public GameObject spawnArea;
    public GameObject projectile;
    GameObject projectileShot;

    void Start()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Vector2 projectileSpawnLocation = new Vector2(spawnArea.transform.position.x, spawnArea.transform.position.y);
        projectileShot = Instantiate(projectile, projectileSpawnLocation,transform.rotation);
        projectileShot.GetComponent<Rigidbody2D>().AddForceX( 200);
        yield return new WaitForSeconds(5);
        StartCoroutine(Shot());
    }
}
