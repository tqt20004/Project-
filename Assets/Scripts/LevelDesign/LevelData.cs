using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Data/Level")]
public class LevelData : ScriptableObject
{
    public GameObject gridPrefab; 
    public string levelName;
    public List<WaveCfg> waves;
    public rank rank;
}
public enum rank
{
    easy,
    medium,
    hard,
    nightmare,
    asian
}