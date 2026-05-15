using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    private float speed = 30f;
    private float acceleration = 1f;
    private void Start()
    {
        point1 = transform;
        var target = GameObject.Find("Target");

        point2 = target.transform;

        StartCoroutine(UpdateBullet());
    }

    float duration = 0.1f;
    
    IEnumerator UpdateBullet()
    {
        yield return new WaitForSeconds(0.25f);
        
        while (true)
        {
            //acceleration *= 1.1f;
            Vector3 targetDir = point2.position - transform.position;
            targetDir.Normalize();

            Vector3 dir = transform.forward + targetDir;
            dir.Normalize();

            float t = 1f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), t);
            //transform.rotation = Quaternion.LookRotation(dir);
            yield return new WaitForSeconds(duration);
            duration *= 0.5f;
        }
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * acceleration * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //acceleration *= 1.1f;
        StartCoroutine(accelcor());
        //Destroy(gameObject);
    }

    IEnumerator accelcor()
    {
        duration = 0.4f;
        acceleration *= 1.3f;
        yield return new WaitForSeconds(0.25f);
        acceleration /= 1.3f;
    }
}