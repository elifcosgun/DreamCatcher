using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        coll = GetComponent<CapsuleCollider>();
    }

    public GameObject mesh;
    public CapsuleCollider coll;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            mesh.SetActive(false);
            coll.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            mesh.SetActive(true);
            coll.enabled = true;
        }
    }
}
