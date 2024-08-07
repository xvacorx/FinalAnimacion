using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlip : MonoBehaviour
{
    public float flipInterval = 1f; 
    private float timer;
    private bool isFlipped = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flipInterval)
        {
            Flip();
            timer = 0f;
        }
    }

    void Flip()
    {
        isFlipped = !isFlipped;

        Vector3 scale = transform.localScale;
        scale.x = isFlipped ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
