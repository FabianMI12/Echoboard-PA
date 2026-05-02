using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnPoint
{
    public Transform point;

    [Header("On Arrival Effects")]
    public bool applyScaleChange = false;
    public Vector3 scaleMultiplier = Vector3.one;

    public bool applyRotation = false;
    public Vector3 targetRotationEuler;
}

public class PawnMovement : MonoBehaviour
{
    [Header("Path Settings")]
    public List<PawnPoint> pathPoints = new List<PawnPoint>();
    public float moveSpeed = 5f;
    public float reachThreshold = 0.05f;

    [Header("Loop Settings")]
    public bool loop = true;
    public bool pingPong = false;

    private int currentIndex = 0;
    private int direction = 1;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     MoveNextStep();
        // }
    }

    void MoveToTarget()
    {
        Transform target = pathPoints[currentIndex].point;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) <= reachThreshold)
        {
            ApplyNodeEffects();
            isMoving = false;
        }
    }

    void ApplyNodeEffects()
    {
        PawnPoint node = pathPoints[currentIndex];

        // 🧍 Scale change
        if (node.applyScaleChange)
        {
            transform.localScale = Vector3.Scale(transform.localScale, node.scaleMultiplier);
        }

        // 🔄 Rotation change
        if (node.applyRotation)
        {
            transform.rotation = Quaternion.Euler(node.targetRotationEuler);
        }
    }

    public void MoveNextStep()
    {
        if (pathPoints.Count == 0 || isMoving) return;

        AdvanceIndex();
        isMoving = true;
    }

    void AdvanceIndex()
    {
        if (pingPong)
        {
            currentIndex += direction;

            if (currentIndex >= pathPoints.Count)
            {
                currentIndex = pathPoints.Count - 2;
                direction = -1;
            }
            else if (currentIndex < 0)
            {
                currentIndex = 1;
                direction = 1;
            }
        }
        else
        {
            currentIndex++;

            if (currentIndex >= pathPoints.Count)
            {
                currentIndex = loop ? 0 : pathPoints.Count - 1;
            }
        }
    }
}