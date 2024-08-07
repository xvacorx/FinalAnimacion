using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    public float scaleAmplitude = 0.1f; // La amplitud del cambio de escala
    public float scaleFrequency = 1f; // La frecuencia del cambio de escala

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calcula la nueva escala
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;
        Vector3 newScale = initialScale * scaleFactor;

        // Aplica la nueva escala
        transform.localScale = newScale;
    }
}
