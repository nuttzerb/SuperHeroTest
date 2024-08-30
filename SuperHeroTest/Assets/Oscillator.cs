using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    float movementFactor;
    [SerializeField] float period = 2f;
    Vector3 StartingPos;

    void Start()
    {

        StartingPos = transform.position;
        period = Random.Range(2f, 5f);

    }
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        {
            float cycle = Time.time / period;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycle * tau); // goes from -1 to +1
            movementFactor = rawSinWave / 2f + 0.5f;
        }
        Vector3 offset = movementVector * movementFactor;
        transform.position = StartingPos + offset;
    }

}
