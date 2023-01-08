using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossInteractionFade : MonoBehaviour
{
    [SerializeField]
    private int fades = 10;
    [SerializeField]
    private float fadeSpeed = 1;

    private SpriteRenderer sprite;
    private int fadeCounts;
    private GameController gameController;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
    }

    public void Teleport()
    {
        gameObject.tag = "Undefined";
        StartCoroutine(TeleportAway(false));
    }
    public IEnumerator TeleportAway(bool fadeIn)
    {
        Color spriteColor = sprite.color;
        if (fadeIn)
        {
            while (sprite.color.a < 1)
            {
                float fadeAmount = spriteColor.a + (fadeSpeed * Time.deltaTime);
                spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, fadeAmount);
                sprite.color = spriteColor;
                yield return null;
            }

            if (fadeCounts < fades)
            {
                StartCoroutine(TeleportAway(false));
                fadeCounts++;
            }
            else
            {
                gameController.HitBossInGame();
                Destroy(gameObject);
            }
        }
        else
        {
            while (sprite.color.a > 0)
            {
                float fadeAmount = spriteColor.a - (fadeSpeed * Time.deltaTime);
                spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, fadeAmount);
                sprite.color = spriteColor;
                yield return null;
            }
            StartCoroutine(TeleportAway(true));
        }
    }
}
