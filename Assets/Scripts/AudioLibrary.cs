using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Data/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public List<AudioData> audioDataList;
}
