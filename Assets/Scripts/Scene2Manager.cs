using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
