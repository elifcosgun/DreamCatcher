using System;
using System.Collections;
using System.Collections.Generic;
using DreamCatcherAssets.Code.Scripts.Player;
using UnityEngine;

public class BedColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().SetCanDoMagic(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().SetCanDoMagic(false);
        }
    }
}
