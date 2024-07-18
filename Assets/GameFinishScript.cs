using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishScript : MonoBehaviour
{

    public Canvas wellDoneCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Time.timeScale = 0;
            
            wellDoneCanvas.gameObject.SetActive(true);
        }
    }
}
