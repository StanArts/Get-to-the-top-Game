using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes_Controller: MonoBehaviour
{
    public GameObject objectToMove;

    [HideInInspector]
    public Animator eyesAnimator;

    void Start()
    {
        eyesAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        objectToMove.transform.localPosition = new Vector3(objectToMove.transform.localPosition.x, Mathf.Sin(Time.time) * 0.1f, objectToMove.transform.localPosition.z);
    }
}