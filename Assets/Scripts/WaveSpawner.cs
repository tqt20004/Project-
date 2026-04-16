using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public LevelData levelData;
    public WaveCfg wave;
    public int enemyAlive;
    bool isWaveEnd = false;
    int index;
    public RunTimeData runTimeData;
    public static Action WaveCleared; 
    private void OnEnable()
    {
        // Khi có bất kỳ AI nào chết
        LevelManager.OnEnemyKilled += EnemyDie;
    }
    // Khi Object này bị tắt hoặc Scene bị hủy
    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi bộ nhớ (Quan trọng!)
        LevelManager.OnEnemyKilled -= EnemyDie;
    }
    // Start is called before the first frame update
    private void Start()
    {
        this.runTimeData = RunTimeData.instance;
        this.levelData = runTimeData.LevelData;
        Debug.Log(runTimeData.LevelData.name);
    }
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
