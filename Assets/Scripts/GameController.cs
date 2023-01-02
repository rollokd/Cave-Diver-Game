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
    private Boss boss;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
}
