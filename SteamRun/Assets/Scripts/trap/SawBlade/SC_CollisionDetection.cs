using UnityEngine;

public class SC_CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            EventManager.Instance.PlayerDeathFunc();
        }
    }
}
