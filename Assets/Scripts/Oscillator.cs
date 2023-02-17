using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} //fixes /by0 (epsilon smallest float)
        float  cycles = Time.time / period; //Continually Growing
        const float tau = Mathf.PI * 2; //Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //Going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2; //recalculate to 0-1 cleanly

        Vector3 offset = movementVector * movementFactor; //movement math
        transform.position = startingPosition + offset; //moving the obj
    }
}
