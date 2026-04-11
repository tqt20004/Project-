using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject currentPlayerPrefab;
    // Start is called before the first frame update
    public static ObjectSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DonDestroyOnLoad(gameObject); // Bật cái này nếu muốn Spawner sống xuyên suốt các Scene
    }
    void Start()
    {
        
    }

    public GameObject SpawnPlayer(RoleConfig cfg)
    {
        // Giả sử RunTimeData là một Singleton hoặc ScriptableObject chứa dữ liệu nâng cấp
        // Ví dụ: RunTimeData.Instance.PlayerHealthBonus = 2.5 (do đã mua áo xịn)
        int currentHpMod = RunTimeData.instance.GetPlayerHealthModifier();

        return SpawnUnit(playerPrefab, cfg, currentHpMod);
    }

    public GameObject SpawnEnemy(RoleConfig cfg)
    {
        // Enemy có thể lấy mod theo Wave hoặc độ khó hiện tại
        int enemyHpMod = RunTimeData.instance.GetEnemyHealthModifier();

        return SpawnUnit(enemyPrefab, cfg, enemyHpMod);
    }

    private GameObject SpawnUnit(GameObject prefab, RoleConfig config, int hpMod)
    {
        GameObject obj = Instantiate(prefab);
        AIBase ai = obj.GetComponentInChildren<AIBase>();
        if (ai != null)
        {
            ai.Init(config, hpMod);
        }
        return obj;
    }

    //public GameObject SpawnPlayer(RoleConfig roleCfg)
    //{

    //    if (playerPrefab != null)
    //    {
    //        GameObject player = Instantiate(playerPrefab);
    //        AIBase aIBase = player.GetComponentInChildren<AIBase>();
    //        if (aIBase != null)
    //        {
    //            aIBase.Init(roleCfg);
    //            aIBase.SetSprite(); // 3. Đổi hình

    //            var hpStat = aIBase.roleStats.GetStat(StatType.health);
    //            hpStat.basePercentValue = 2; // đặt tạm 2 , sau này lấy từ RunTimeData
    //            hpStat.currentValue = hpStat.GetFinalValue();
    //        }
    //        return player;
    //    }
    //    return null;
    //}
    //public GameObject SpawnEnemy(RoleConfig RCfg)
    //{

    //    if (enemyPrefab != null)
    //    {
    //        GameObject enemy = Instantiate(enemyPrefab);
    //        AIBase AIBase = enemy.GetComponentInChildren<AIBase>();
    //        if (AIBase != null)
    //        {
    //            AIBase.Init(RCfg);
    //            AIBase.SetSprite(); // 3. Đổi hình

    //            var hpStat = AIBase.roleStats.GetStat(StatType.health);
    //            hpStat.basePercentValue = 1; // đặt tạm 2 , sau này lấy từ RunTimeData
    //            hpStat.currentValue = hpStat.GetFinalValue();
    //        }
    //        return enemy;
    //    }
    //    return null ;
    //}
}
