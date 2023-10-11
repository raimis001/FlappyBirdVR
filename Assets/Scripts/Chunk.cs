using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chunk : MonoBehaviour
{
    public Transform[] columns;
    public Vector2 delta = new Vector2(-1.75f,2);

    private void Start()
    {
        Rebuild();
    }

    internal void Rebuild()
    {
        Vector3 pos;
        foreach (Transform t in columns)
        {
            pos = t.localPosition;
            pos.y = Random.Range(delta.x, delta.y);
            t.localPosition = pos;
        } 
    }

    
}
