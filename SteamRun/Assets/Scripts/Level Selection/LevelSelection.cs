using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private int world;
    [SerializeField] private int level;
    [SerializeField] private List<Sprite> sprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isOpen =false;
    [SerializeField] private bool isWorldDoor;
    [SerializeField] private TMP_Text doorText;
    private Color color;


    bool playerInFront =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInFront = true;
            if (this.enabled == true)
            {
                StartCoroutine(ShowText(0.5f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInFront = false;
            if (this.enabled == true)
            {
                StartCoroutine(HideText(0.5f));
            }
        }
    }

    private void Start()
    {
        color = doorText.color;
        InvisibleText();
        EventManager.Instance.Interact += GoToLevel;
        if (isWorldDoor == true)
        {
            doorText.text = "WORLD" + world.ToString();
        }
        else
        {
            doorText.text = "LEVEL" + level.ToString();
        }

        if (GameMode.currentMaxWorld > world )
        {
            isOpen = true;
            spriteRenderer.sprite = sprite[1];
        }
        else if (GameMode.currentMaxWorld == world && GameMode.currentMaxLevel >= level)
        {
            isOpen = true;
            spriteRenderer.sprite = sprite[1];
        }
        else
        {
            spriteRenderer.sprite = sprite[0];
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.Interact -= GoToLevel;
    }

    void GoToLevel()
    {
        if (nextLevel != null && playerInFront == true && isOpen ==true)
        {
            SceneManager.LoadScene(nextLevel);
        }       
    }
    IEnumerator ShowText(float duration)
    {
        float time = 0;
        while (time < duration && doorText.color.a < 1)
        {
            time += Time.deltaTime;
            color.a = time / duration;
            doorText.color = color;
            yield return null;
        }
    }
    IEnumerator HideText(float duration)
    {
        float time = 0;
        while (time < duration && doorText.color.a >0)
        {
            time += Time.deltaTime;
            color.a = 1f - (time * 4) /1;
            doorText.color = color;
            yield return null;
        }
    }
    private void InvisibleText()
    {
        color.a = 0f;
        doorText.color = color;
    }
}
