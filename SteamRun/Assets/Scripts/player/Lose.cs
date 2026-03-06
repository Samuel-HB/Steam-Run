using UnityEngine;

public class Lose : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            EventManager.Instance.PlayerDeathFunc();
        }
    }
}
