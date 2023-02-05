using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_Platform : MonoBehaviour
{
    public float speed;
    public float distance;
    public float smoothFactor;

    private int currentIndex;
    private int listLength;

    public bool usingFixedPoints;
    public Transform[] checkpoints;
    public List<Vector2> targetPositions;
    Vector2 velocity;
    void Start()
    {
        currentIndex = 0;
        listLength = checkpoints.Length;
        targetPositions = new List<Vector2>();
        foreach(Transform checkpoint in checkpoints)
        {
            targetPositions.Add(new Vector2(checkpoint.position.x, checkpoint.position.y));
        }
    }

    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPositions[currentIndex], ref velocity, smoothFactor, speed);
        float currentDistance = Vector2.Distance(transform.position, targetPositions[currentIndex]);
        if(currentDistance < distance)
        {
            if (currentIndex < listLength - 1) currentIndex++;
            else currentIndex = 0;
        }
    }
}
