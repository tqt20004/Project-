using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerVisual : AttackVisualBase
{
    public LineRenderer line;
    public float duration = 0.1f;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    public override void PlayEffect(Vector3 start, Vector3 end, float range = 0)
    {
        Debug.Log("already called PLayEffect");

        if (line == null) return;
        H(start, end);
        Debug.Log("already Draw");
        //StopAllCoroutines(); // Tránh chồng chéo tia laser
        //StartCoroutine(LaserRoutine(start, end));
    }

    private System.Collections.IEnumerator LaserRoutine(Vector3 start, Vector3 end)
    {
        line.enabled = true;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        yield return new WaitForSeconds(duration);
        line.enabled = false;
    }
    public void H(Vector3 s ,Vector3 e)
    {
        line.SetPosition(0, s);
        line.SetPosition(1, e);
    }
}
