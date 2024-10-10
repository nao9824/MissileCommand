using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

#if UNITY_EDITOR
[CustomEditor(typeof(Explosion))]
#endif

public class TowerExplosion : Explosion
{
    public override void Exp()
    {
        Vector3 scale;
        scale = transform.localScale;
        scale.x += 2.0f * Time.deltaTime;
        transform.localScale = scale;
    }
}
