using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform folowingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = .1f;
    private bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    void Start()
    {
        if (!folowingTarget)
        {
            folowingTarget = Camera.main.transform;
        }
        targetPreviousPosition = folowingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = folowingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
        {
            delta.y = 0;
        }

        targetPreviousPosition = folowingTarget.position;
        transform.position += delta * parallaxStrength;
        
    }
}
