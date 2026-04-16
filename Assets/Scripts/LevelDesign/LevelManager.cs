using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject currentMap;
    public static event Action<Vector2> OnEnemyKilled;
    public static event Action<int> OnExperienceChanged;
    public int difficultDigital;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void OnEnable()
    {
        // When it got any enemyDie
        AIBase.OnDie += HandleEntityDeath;
    }

    // Automatically Turn Off when GameObj is destroyed
    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi bộ nhớ (Quan trọng!)
        AIBase.OnDie -= HandleEntityDeath;
    }
    private void Start()
    {
        difficultDigital = GetDifficulty(RunTimeData.instance.LevelData.rank);
    }
    private int GetDifficulty(rank rank)
    {
        return rank switch
        {
            rank.easy => 1,
            rank.medium => 2,
            rank.hard => 3,
            rank.nightmare => 4,
            rank.asian => 10,
            _ => 1 //Default = 1 
        };
    }
    void HandleEntityDeath(AIBase aibase,Vector2 deathPos)
    {
        if(aibase.roleConfig.isPlayer == true) { PlayerDie(); }
        if(aibase.roleConfig.isPlayer == false) { E(deathPos); }
    }

    public void SpawnMap()
    {
        Instantiate(currentMap);
    }
    public void PlayerDie()
    {
        Debug.Log("xog");
        GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
    }
    public void E(Vector2 deathPos)
    {
        OnEnemyKilled?.Invoke(deathPos);
        HandleExperience();
    }
    //để tạm ở đây...
    public void HandleExperience()
    {
        if (RunTimeData.instance != null)
        {
            int ex_amount = UnityEngine.Random.Range(5, 25) * difficultDigital;
            RunTimeData.instance.curGameData.player_Experience += ex_amount;
            Debug.Log("now we got :" + RunTimeData.instance.curGameData.player_Experience + "_ex ");
            OnExperienceChanged?.Invoke(ex_amount);
        }
    }
}
