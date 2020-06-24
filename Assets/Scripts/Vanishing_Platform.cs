using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanishing_Platform : MonoBehaviour
{
    private float timeToShowOff;
    public float showTime;

    public bool isActive = true;

    public GameObject disappearingPlatform;

    void Start()
    {
        timeToShowOff = showTime;
    }

    void Update()
    {
        if (isActive)
        {
            timeToShowOff -= Time.deltaTime;

            if (timeToShowOff <= 0)
            {
                disappearingPlatform.SetActive(false);
                isActive = false;
                StartCoroutine(Enable());
            }
        }
    }

    IEnumerator Enable()
    {
        yield return new WaitForSeconds(10f);
        isActive = true;
        timeToShowOff = showTime;

        disappearingPlatform.SetActive(true);
    }
}
