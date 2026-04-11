using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public LevelData levelData;
    public Wave wave;
    public int enemyAlive;
    bool isWaveEnd = false;
    int index;
    public static Action WaveCleared; 
    private void OnEnable()
    {
        // Khi có bất kỳ AI nào chết
        AIBase.OnDie += EnemyDie;
    }
    // Khi Object này bị tắt hoặc Scene bị hủy
    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi bộ nhớ (Quan trọng!)
        AIBase.OnDie -= EnemyDie;
    }
    // Start is called before the first frame update

    public void Spawn()
    {
        for (int i = 0; i < wave.count; i++)
        {
            if (wave == null || wave.enemyRoleCfg == null) return;
            GameObject enemy = ObjectSpawner.Instance.SpawnEnemy(wave.enemyRoleCfg);
            if (enemy != null)
            {
                enemyAlive++;
                Debug.Log("Spawned 1 enemy : " + wave.enemyRoleCfg.name);
            }
            else
            {
                Debug.LogError("ObjectSpawner tra ve NULL!");
            }
            isWaveEnd = false;
        }
    }
    public void Spawn1()
    {
        if (index >= levelData.waves.Count)
        {
            Debug.Log("no more Level");
            WaveCleared?.Invoke();
            return;
        }
        wave = levelData.waves[index];
        index++;
        Spawn();
    }
    public void EnemyDie(Vector2 x)
    {
        enemyAlive--;
        CheckWaveEnd();
    }
    public void CheckWaveEnd()
    {
        if (enemyAlive <= 0)
        {
            isWaveEnd = true;
            StartCoroutine(WaitForNextSpawn());
        }
    }
    IEnumerator WaitForNextSpawn()
    {
        yield return new WaitForSeconds(5f);
        Spawn1 ();
    }
    
}
