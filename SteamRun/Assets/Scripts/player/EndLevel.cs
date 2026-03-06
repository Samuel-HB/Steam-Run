using UnityEngine;

public class EndLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.Instance.EndLevelFunc();
        }
    }
}
