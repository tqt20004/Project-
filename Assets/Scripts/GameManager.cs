using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using NavMeshPlus.Components;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallTilemap;
    public GameObject itemPrefab;
    public GameDatabase Database;
    public static GameManager Instance { get; private set; }
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState;
    public CinemachineVirtualCamera VirtualCamera;
    public RoleConfig curRoleConfigPlayer;
    // Trong phần khai báo biến của GameManager
    [SerializeField]
    public NavMeshPlus.Components.NavMeshSurface surfaceNav;
    public WaveSpawner waveSpawner;
    [Header("Game Data")]
    public int Score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    private void OnEnable()
    {
        WaveSpawner.WaveCleared += SetGameOver ;
    }
    private void OnDisable()
    {
        WaveSpawner.WaveCleared -= SetGameOver ;
    }
    private void Start()
    {
        GameData data = SaveLoadSystem.LoadGame();
        foreach (int z in data.inventory)
        {
            Debug.Log(z + "Loaded");
        }
        //InventoryManager.instance.LoadItemToInventory(data);    
        ChangeState(GameState.MainMenu);
        //test
        AutomaticSpawnEquipedItem();
    }

    private void Update()
    {
        
        // Chạy logic theo từng trạng thái
        switch (CurrentState)
        {
            case GameState.MainMenu: UpdateMainMenu(); break;
            case GameState.Playing: UpdatePlaying(); break;
            case GameState.Paused: UpdatePaused(); break;
            case GameState.GameOver: UpdateGameOver(); break;
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        // Xử lý sự kiện "Một lần" khi chuyển State (Enter State)
        switch (CurrentState)
        {
            case GameState.MainMenu: SetupMainMenu(); break;
            case GameState.Playing: SetupPlaying(); break;
            case GameState.Paused: Time.timeScale = 0; break;
            case GameState.GameOver: SetupGameOver(); break;
        }
    }
    // 1. MAIN MENU
    void SetupMainMenu() 
    {
        Debug.Log("1");
        
    }
    void UpdateMainMenu()
    { 
           Debug.Log("2");
        ChangeState(GameState.Playing);
    }
    // 2. PLAYING
    void SetupPlaying()
    {
        Debug.Log("3");
        Time.timeScale = 1;
        Score = 0;
        Debug.Log("chạy");
        //LevelManager.Instance.SpawnMap();
        if (RunTimeData.instance != null && RunTimeData.instance.curRoleCfg != null)
        {
            curRoleConfigPlayer = RunTimeData.instance.curRoleCfg;
        }
        GameObject temp = ObjectSpawner.Instance.SpawnPlayer(curRoleConfigPlayer);
        //GameObject temp2 = ObjectSpawner.Instance.SpawnEnemy();
        waveSpawner.Spawn1();
        VirtualCamera.Follow = temp.transform;
            CamFollow(temp);
    }
    void UpdatePlaying()
    {
        if (Input.GetKeyDown(KeyCode.P)) ChangeState(GameState.Paused);
        if (Input.GetKeyDown(KeyCode.I)) ChangeState(GameState.GameOver);
    }

    // 3. PAUSED
    void UpdatePaused() 
    { 
        if (Input.GetKeyDown(KeyCode.P)) 
        { 
            Time.timeScale = 1; ChangeState(GameState.Playing);
        }
    }

    // 4. GAME OVER
    void SetupGameOver()
    {
        Debug.Log("End! Điểm: " + Score);
        AddMoney();

        BackToPreviousScene();
    }
    void UpdateGameOver() 
    { 
        if (Input.GetKeyDown(KeyCode.R)) ChangeState(GameState.Playing); 
    }
    void SpawnItem(int id)
    { 
        // đưa id vào sẽ tự spawn ra trên Game
        ItemDataSO weaponDataSO = Database.GetWeaponByID(id);
        if (weaponDataSO == null) return;
        //Vector3 vector3 = new Vector3(0, 7 , 0 );
        GameObject z = Instantiate(itemPrefab);
        Item item =  z.GetComponent<Item>() ?? z.AddComponent<Item>();
        item.GetSO(weaponDataSO);
    }

    // Hàm tiện ích để khảm vào Script khác
    public void AddScore(int pts) => Score += pts;

    public void CamFollow(GameObject entity)
    {
        if(VirtualCamera != null)
        {
            VirtualCamera.Follow = entity.transform;
            VirtualCamera.LookAt = entity.transform;
        }
    }
    //public void Bake()
    //{
    //    if (surfaceNav == null)
    //    {
    //        Debug.LogError("NavMeshSurface chưa được gán!");
    //        return;
    //    }

    //    Physics2D.SyncTransforms();
    //    surfaceNav.RemoveData();
    //    surfaceNav.BuildNavMesh();

    //    Debug.Log("Bake called");
    //}
    public void AutomaticSpawnEquipedItem()
    {
        foreach (var  i  in RunTimeData.instance.itemEquipedList)
        {
            SpawnItem(i);
        }
    }
    public void AddMoney()
    {
        foreach (var i in InventoryManager.instance.itemSlot)
        {
            if(i.id == 22)
            {
                Score += i.quantity;
            }
        }
        RunTimeData.instance.AddMoney(Score);
        Debug.Log("đã + : " + Score + " money");
        RunTimeData.instance.SaveGame();
    }
    public void SetGameOver()
    {
        ChangeState(GameState.GameOver);
    }
    public void BackToPreviousScene()
    {
        SceneManager.LoadScene("2");
    }
    public void SaveGame()
    {
        RunTimeData.instance?.SaveGame();
    }
}