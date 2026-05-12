using System;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    public float radius = 1;

    public List<Boid> neighbors = new List<Boid>();


    void Init()
    {
        if (BoidManager.Instance != null)
            velocity = transform.forward * BoidManager.Instance.maxSpeed;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        FindNeighbors();

        velocity += Cohesion() * BoidManager.Instance.cohensionWeight;
        velocity += Alignment() * BoidManager.Instance.alignmentWeight;
        velocity += Separation() * BoidManager.Instance.separationWeight;

        if (velocity.magnitude > BoidManager.Instance.maxSpeed)
            velocity = velocity.normalized * BoidManager.Instance.maxSpeed;

        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    void FindNeighbors() // 근접한 이웃 탐색
    {
        neighbors.Clear();

        foreach (var boid in BoidManager.Instance.boids)
        {
            Vector3 diff = boid.transform.position - transform.position;

            if (boid == this)
                continue;

            if (diff.sqrMagnitude < Mathf.Pow(BoidManager.Instance.neighborDst, 2))
                neighbors.Add(boid);
            
        }
    }

    Vector3 Cohesion() // 이웃들의 중앙 방향
    {
        Vector3 centerDir = Vector3.zero;

        if (neighbors.Count > 0)
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                centerDir += neighbors[i].transform.position - transform.position;
            }

            centerDir /= neighbors.Count;
            centerDir.Normalize();
        }

        return centerDir;
    }

    Vector3 Alignment() // 이웃들의 평균 방향
    {
        Vector3 AlignDir = transform.forward;

        if (neighbors.Count > 0)
        {
            for (int i = 0; i < neighbors.Count; i++)
                AlignDir += neighbors[i].transform.forward;
            
            AlignDir /= neighbors.Count;
            AlignDir.Normalize();
        }

        return AlignDir;
    }

    Vector3 Separation() // 이웃들과 분리
    {
        Vector3 separationDir = Vector3.zero;

        if (neighbors.Count > 0)
        {
            for (int i = 0; i < neighbors.Count; i++)
                separationDir += transform.position - neighbors[i].transform.position;
            
            separationDir /= neighbors.Count;
            separationDir.Normalize();
        }
        
        return separationDir;
    }
}