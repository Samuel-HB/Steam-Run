using UnityEngine;

public class ActivateDeactivate : MonoBehaviour
{
    [SerializeField] private GameObject goSprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private PlayerMovementTest movementScript;


    private void Start()
    {
        EventManager.Instance.PlayerDeath += Deactivate;
        EventManager.Instance.RestartLevel += Activate;
        EventManager.Instance.AbleToPlay += ActivateInput;
        movementScript.enabled = false;
    }

    private void Deactivate()
    {
        goSprite.SetActive(false);
        rb.linearVelocityX = 0f;
        rb.linearVelocityY = 0f;
        rb.gravityScale = 1.9f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        coll.enabled = false;
        movementScript.enabled = false;
    }

    private void Activate()
    {
        goSprite.SetActive(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.enabled = true;
    }

    private void ActivateInput()
    {
        movementScript.enabled = true;
    }

    private void OnDestroy()
    {
        EventManager.Instance.PlayerDeath -= Deactivate;
        EventManager.Instance.RestartLevel -= Activate;
        EventManager.Instance.AbleToPlay -= ActivateInput;
    }
}
