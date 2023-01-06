using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int feelings {get; private set;}
    public int bossHealth { get; private set; }

    public bool bossFight = false;

    [SerializeField]
    private int damageAmountToBoss;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Boss boss;
    [SerializeField]
    private GameObject waterCollision;

    private float multiplierValues = 1.5f;
    private PlayerMovement playerMovement;
    


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void Negative()
    {
        feelings--;
    }

    public void Positive()
    {
        feelings++;
    }

    public void HitBossInGame()
    {
        bossHealth -= damageAmountToBoss;
    }

    public void StartBossFight()
    {
        boss.SetHealth(bossHealth);
    }

    public void DoubleJump(bool chosen)
    {
        boss.doubleJump = !chosen;

        if (chosen)
            playerMovement.AddJump();

        FindObjectOfType<Weapon>().rocket = !chosen;
    }

    public void HealthChosen(bool health)
    {
        boss.healthMultiplier = health ? 1 : multiplierValues;
        player.healthMultiplier = !health ? 1 : multiplierValues;

        boss.damageMultiplier = !health ? 1 : multiplierValues;
        player.damageMultiplier = health ? 1 : multiplierValues;
    }

    public void WalkOnWater()
    {
        waterCollision.SetActive(true);
    }

    public void RunFaster(bool chosen)
    {
        if (chosen)
            playerMovement.IncreaseMovementSpeed(3);

        playerMovement.jetPack = !chosen;
    }

    [SerializeField]
    private GameObject blackOutSquare;

    public void Fade(bool black, int speed, string newScene = null)
    {
        StartCoroutine(FadeBlackOutSquare(black, speed, newScene));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack, int fadeSpeed, string newScene = null)
    {
        Color objectColour = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;
        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColour.a + (fadeSpeed * Time.deltaTime);

                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColour;
                yield return null;
            }
            SceneManager.LoadScene(newScene);
            Fade(false, fadeSpeed);
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColour.a - (fadeSpeed * Time.deltaTime);

                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColour;
                yield return null;
            }
        }
    }

    public void EnterBossFight()
    {
        bossFight = true;
    }

    public void DieInBoss()
    {
        if (feelings >= 0)
        {
            Spare();
        }
        else
        {
            BossKills();
        }
    }

    public void KillBoss()
    {
        if (feelings >= 0)
        {
            SpareBoss();
        }
        else
        {
            FinishBoss();
        }
    }

    private void SpareBoss()
    {
        Debug.Log("Spare the boss");
        SceneManager.LoadScene("Spare Boss Cutscene");
    }

    private void FinishBoss()
    {
        Debug.Log("Finish the boss");
    }

    private void Spare()
    {
        Debug.Log("Boss spares your life");
        SceneManager.LoadScene("Spare Player Cutscene");
    }

    private void BossKills()
    {
        Debug.Log("Boss kills you");
    }
}
