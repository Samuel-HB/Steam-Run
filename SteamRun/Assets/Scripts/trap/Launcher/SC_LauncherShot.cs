using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SC_LauncherShot : MonoBehaviour
{
    public GameObject spawnArea;
    public GameObject projectile;
    GameObject projectileShot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Shot());
    }

    // Update is called once per frame
    void Update()
    {
        
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
