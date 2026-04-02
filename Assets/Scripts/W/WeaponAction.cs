using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetSelector
{
    AIBase GetTarget(Transform seeker, float range);
}