using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int bossHealthChange;
    [HideInInspector]
    public bool bossFight;
    [HideInInspector]
    public bool paused;

    [SerializeField]
    private Player player;
    [SerializeField]
    private int damageAmountToBoss = 5;
    [SerializeField]
    private GameObject waterCollision;
    [SerializeField]
    private GameObject blackOutSquare;

    [Header("Chests")]
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

    [Header("Speech")]
    [SerializeField]
    private GameObject regretThat;
    [SerializeField]
    private GameObject thanksSpeech;


    private Boss boss;
    private PlayerMovement playerMovement;
    private int chestNumber = 1;
    private bool left;
    private bool right;
    private bool bossDouble;
    private int playerJumps;
    private int feelings = 1;
    private bool speed;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!paused)
            return;

        if (Input.GetButtonDown("LeftChoice"))
            left = true;

        if (Input.GetButtonDown("RightChoice"))
            right = true;

    }

    public void HitBossInGame()
    {
        bossHealthChange -= damageAmountToBoss;
        feelings--;
    }

    public void StartBossFight()
    {
        StartCoroutine(LoadBossBattleScene());
    }

    private IEnumerator LoadBossBattleScene()
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync("Boss Battle", LoadSceneMode.Single);
        while(!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        SetUpBossBattle();
    }

    private void SetUpBossBattle()
    {
        player = FindObjectOfType<Player>();
        playerMovement = player.GetComponent<PlayerMovement>();
        boss = FindObjectOfType<Boss>();

        bossFight = true;
        boss.health += bossHealthChange;

        //First choice
        playerMovement.maxJumps = playerJumps;
        boss.doubleJump = bossDouble;

        player.GetComponent<Weapon>().rocket = bossDouble;
        boss.GetComponent<BossWeapon>().SetRocket(!bossDouble);

        //Second choice doesn't effect boss fight

        //Third choice
        if (speed)
        {
            playerMovement.IncreaseMovementSpeed(3);
        }
        else
        {
            boss.IncreaseMovementSpeed(3);
        }

        playerMovement.jetPack = !speed;
        boss.jetPack = speed;
    }

    private void DoubleJump(bool chosen)
    {
        bossDouble = !chosen;

        if (chosen)
        {
            playerMovement.AddJump();
            playerJumps++;
        }

        FindObjectOfType<Weapon>().rocket = !chosen;
    }

    private void RunFaster(bool chosen)
    {
        speed = chosen;

        if (chosen)
            playerMovement.IncreaseMovementSpeed(3);

        playerMovement.jetPack = !chosen;
    }

    public void Fade(bool black, int speed, string newScene = null)
    {
        StartCoroutine(FadeBlackOutSquare(black, speed, newScene));
    }

    private IEnumerator FadeBlackOutSquare(bool fadeToBlack, int fadeSpeed, string newScene = null)
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

    public void DieInBoss()
    {
        if (feelings >= 0)
        {
            SceneManager.LoadScene("Spare Player Cutscene");
        }
        else
        {
            SceneManager.LoadScene("Kill Player Cutscene");
        }
    }

    public void KillBoss()
    {
        if (feelings >= 0)
        {
            SceneManager.LoadScene("Spare Boss Cutscene");
        }
        else
        {
            SceneManager.LoadScene("Kill Boss Cutscene");
        }
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
                waterCollision.SetActive(true);

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
        thanksSpeech.SetActive(false);
        regretThat.SetActive(true);
        StartCoroutine(RegretTimer(4));
    }

    private IEnumerator RegretTimer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        regretThat.SetActive(false);
    }
}
