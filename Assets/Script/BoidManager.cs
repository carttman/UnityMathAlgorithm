using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoidManager : MonoBehaviour
{
    public static BoidManager Instance;
    
    public List<Boid> boids = new List<Boid>();
    
    public GameObject prefab;
    
    public float radius;
    public float count;
    
    public Vector3 velocity;
    public float cohensionWeight = 5;
    public float alignmentWeight = 5;
    public float separationWeight = 5;
    public float maxSpeed = 2;
    public float neighborDst = 10;
    private void Awake()
    {
        if( Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var go =Instantiate(prefab, transform.position + Random.insideUnitSphere * radius, Random.rotation);
            boids.Add(go.GetComponent<Boid>());
        }
    }
}
