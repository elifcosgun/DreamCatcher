using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "InteractableObject")]
public class InteractableObjectData : ScriptableObject
{
    public IntractionType objectsInteractionType;

    public Transform startingPosition;
    public Transform endingPosition;
}
