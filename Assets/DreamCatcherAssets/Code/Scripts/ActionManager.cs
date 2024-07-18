using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ActionManager : MonoBehaviour
{
    //public static Action<int, bool> OnPrimaryAbilityChanged;
    //public static void Fire_OnPrimaryAbilityChanged(int mainAbilityIndex, bool isAbility) { OnPrimaryAbilityChanged?.Invoke(mainAbilityIndex, isAbility); }

    public void OnDestroy()
    {
        FieldInfo[] fieldInfos = this.GetType().GetFields();
        // Debug.Log("fieldInfos " + fieldInfos.Length);
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            // Debug.Log(fieldInfos[i].Name);

            fieldInfos[i].SetValue(this, null);
        }
    }
}
