using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public InteractableObjectData objectData;
    public MissionScriptable objectMission;

    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        switch (objectData.objectsInteractionType)
        {
            case IntractionType.Openable:
                Open();
                break;
            case IntractionType.Collectable:
                Collect();
                break;
            case IntractionType.Hideable:
                Hide();
                break;
            default:
                break;
        }
    }

    private void Open()
    {
        //objectData.startingPosition = gameObject.transform;

        //gameObject.transform.DOMove(objectData.endingPosition.position, 1f);
        //gameObject.transform.DORotate(objectData.endingPosition.rotation.eulerAngles, 1f);

        //objectData.endingPosition = objectData.startingPosition;
        Debug.Log(gameObject.name + " opened");
        _animator.Play("Open");
    }
    private void Collect()
    {
        gameObject.transform.DOScale(0, 1.1f);
        gameObject.transform.DOJump(Player.Instance.transform.position, 1f, 1, 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void Hide()
    {

    }
}

public enum IntractionType
{
    Openable,
    Collectable,
    Hideable
}
