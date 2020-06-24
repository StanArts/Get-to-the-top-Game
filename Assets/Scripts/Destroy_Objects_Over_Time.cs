using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Objects_Over_Time : MonoBehaviour
{
    public float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}