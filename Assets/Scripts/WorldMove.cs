using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMove : MonoBehaviour
{
    public float speed = 1;

    public Chunk[] chunks;
    public bool active {  get; set; }

    void Update()
    {
        if (!active)
            return;

        transform.Translate(transform.forward * Time.deltaTime * -speed, Space.Self);
        foreach (Chunk t in chunks)
        {
            if (t.transform.position.z < -100)
            {
                Vector3 pos = t.transform.position;
                pos.z = 50;
                t.transform.position = pos;
                t.Rebuild();
            }
        }
    }

    public void Rebuild()
    {
        transform.position = Vector3.zero;
        for (int i = 0; i < chunks.Length; i++) 
        {
            float z = -50 + i * 50f;
            chunks[i].transform.localPosition = new Vector3(0, 0, z);
        }
    }


}
