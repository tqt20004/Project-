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

    public GameObject SpawnPlayer(RoleConfig roleCfg)
    {
        
        if (playerPrefab != null)
        {
            GameObject player = Instantiate(playerPrefab);
            AIBase aIBase = player.GetComponentInChildren<AIBase>();
            if (aIBase != null)
            {
                Debug.Log(roleCfg.name+"456");
                aIBase.roleConfig = roleCfg;
                aIBase.SetSprite();
            }
            return player;
        }
        return null;
    }
    public GameObject SpawnEnemy()
    {
        
        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            return enemy;
        }
        return null ;
    }
}
