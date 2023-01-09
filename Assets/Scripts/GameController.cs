using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int bossHealth = 30;
    public bool bossFight = false;
    public bool paused = false;

    [SerializeField]
    private int damageAmountToBoss;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Boss boss;
    [SerializeField]
    private GameObject waterCollision;
    [SerializeField]
    private GameObject blackOutSquare;
    [SerializeField]
    private GameObject chest1;
    [SerializeField]
    private GameObject chest2;
    [SerializeField]
    private GameObject chest3;
    [SerializeField]
    private GameObject chestUI;
    [SerializeField]
    private GameObject chestObject1;
    [SerializeField]
    private GameObject chestObject2;
    [SerializeField]
    private GameObject chestObject3;
    [SerializeField]
    private GameObject regretThat;


    private PlayerMovement playerMovement;
    private int chestNumber = 1;
    private bool left;
    private bool right;
    private bool bossDouble = false;
    private int playerJumps = 0;
    private bool rocket;
    private int feelings = 1;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("LeftChoice") && paused)
            left = true;

        if (Input.GetButtonDown("RightChoice") && paused)
            right = true;

    }

    public void HitBossInGame()
    {
        Debug.Log(feelings + " feelings and " + bossHealth + " health");
        bossHealth -= damageAmountToBoss;
        feelings--;
        Debug.Log(feelings + " feelings and " + bossHealth + " health");
    }

    public void StartBossFight()
    {
        boss.SetHealth(bossHealth);
    }

    public void DoubleJump(bool chosen)
    {
        bossDouble = !chosen;

        if (chosen)
        {
            playerMovement.AddJump();
            playerJumps++;
        }

        rocket = !chosen;

        FindObjectOfType<Weapon>().rocket = !chosen;
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
        SceneManager.LoadScene("Kill Boss Cutscene");
    }

    private void Spare()
    {
        Debug.Log("Boss spares your life");
        SceneManager.LoadScene("Spare Player Cutscene");
    }

    private void BossKills()
    {
        Debug.Log("Boss kills you");
        SceneManager.LoadScene("Kill Player Cutscene");
    }

    public void Chest()
    {
        // Stop game and make selections work
        StartCoroutine(WaitForInput());
        Time.timeScale = 0;
        paused = true;

        // Activate specific options
        if (chestNumber == 1)
        {
            chest1.SetActive(true);
        }
        else if (chestNumber == 2)
        {
            chest2.SetActive(true);
        }
        else if (chestNumber == 3)
        {
            chest3.SetActive(true);
        }
        chestUI.SetActive(true);
    }

    private IEnumerator WaitForInput()
    {
        while(!left && !right)
        {
            yield return null;
        }
        
        if (chestNumber == 1)
        {
            DoubleJump(left);
            Destroy(chestObject1);
        }
        else if (chestNumber == 2)
        {
            if (left)
                WalkOnWater();

            Destroy(chestObject2);
        }
        else if (chestNumber == 3)
        {
            RunFaster(left);
            Destroy(chestObject3);
        }

        left = false;
        right = false;

        chest1.SetActive(false);
        chest2.SetActive(false);
        chest3.SetActive(false);
        chestUI.SetActive(false);

        Time.timeScale = 1;
        paused = false;
        Debug.Log("Choice made");
        chestNumber++;
    }

    public void RegretThat()
    {
        regretThat.SetActive(true);
        StartCoroutine(RegretTimer(4));
    }

    private IEnumerator RegretTimer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        regretThat.SetActive(false);
    }
}
