using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int feelings {get; private set;}
    public int bossHealth { get; private set; }

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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
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
}
