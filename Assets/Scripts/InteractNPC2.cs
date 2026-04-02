using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC2 : MonoBehaviour
{
    public GameObject joinGamePanel;
    // Start is called before the first frame update
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joinGamePanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joinGamePanel.SetActive(false);
        }
    }
}
