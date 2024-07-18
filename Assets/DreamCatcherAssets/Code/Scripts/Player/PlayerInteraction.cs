using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    private CustomInputs _inputPlayer;
    private Vector3 _clickPosition;

    private void Awake()
    {
        _inputPlayer = new CustomInputs();
    }

    private void OnClick(InputAction.CallbackContext value)
    {
        _clickPosition = Mouse.current.position.ReadValue();

        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(_clickPosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Object"), QueryTriggerInteraction.Collide))
            {
                if (hit.collider.CompareTag("Object") && 
                    hit.collider.gameObject.TryGetComponent<InteractableObject>(out InteractableObject interactable))
                {
                    Debug.Log(Mathf.Abs(Vector3.Distance(gameObject.transform.position, hit.point)));
                    if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, hit.point)) > interactionDistance)
                    {
                        //move
                    }
                    else
                    {
                        interactable.Interact();
                        int tempCount = int.Parse(cursedObjectCount.text);
                        tempCount--;
                        cursedObjectCount.text = tempCount.ToString();

                    }
                }
            }
        }
    }

    public TextMeshProUGUI cursedObjectCount;

    private void OnEnable()
    {
        _inputPlayer.PlayerInput.Enable();
        _inputPlayer.PlayerInput.LeftMouseClick.started += OnClick;

    }

    private void OnDisable()
    {
        _inputPlayer.PlayerInput.Disable();
        _inputPlayer.PlayerInput.LeftMouseClick.started -= OnClick;

    }
}
