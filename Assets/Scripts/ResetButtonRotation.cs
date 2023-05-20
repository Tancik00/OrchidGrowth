using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonRotation : MonoBehaviour
{
    public void ResetRotation(Transform obj)
    {
        var objRotation = obj.rotation;
        objRotation.z = 0f;
        obj.rotation = objRotation;
    }
}
