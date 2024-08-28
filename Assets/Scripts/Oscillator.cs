using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movement_vector;
    [SerializeField] [Range(0,1)] float movement_factor;
    [SerializeField] float cycle_period = 2f;
    Vector3 starting_vector;
    void Start()
    {
        starting_vector = transform.position;
    }

    void Update()
    {
        if (cycle_period >= Mathf.Epsilon)
        {
            float cycle_time = Time.time / cycle_period;
            movement_factor = Mathf.Sin(cycle_time);
            transform.position = starting_vector + movement_vector * movement_factor;
        }
    }
}
