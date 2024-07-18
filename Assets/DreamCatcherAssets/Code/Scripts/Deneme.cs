using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Deneme : MonoBehaviour
{
    public Transform EndPosition;
    private void Start()
    {
        gameObject.GetComponent<Transform>().DOJump(EndPosition.position, 5f, 2, 1.5f);
    }
}
