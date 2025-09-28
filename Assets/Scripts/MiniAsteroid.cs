using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroid : MonoBehaviour
{
    public float speed = 5;
    public float maxLifeTime = 5;
    public Vector3 direction;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
