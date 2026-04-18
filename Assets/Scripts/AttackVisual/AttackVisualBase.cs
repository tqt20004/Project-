using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackVisualBase : MonoBehaviour
{
    public abstract void PlayEffect(Vector3 start, Vector3 end, float range = 0);
}
