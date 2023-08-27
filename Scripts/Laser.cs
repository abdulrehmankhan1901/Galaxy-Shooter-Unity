using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _speed = 8;
    private float _y_bound = 7.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLaser();

        // destroy the laser if it goes of out of bounds
        if (transform.position.y > _y_bound)
        {
            Destroy(gameObject);
        }
    }

    void MoveLaser()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
