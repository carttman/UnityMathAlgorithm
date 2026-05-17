using System;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{
    private float moveSpeed = 5f;
    public GameObject gameOver;

    void Update()
    {
       Move();
    }
    
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<SecondBullet>())
        {
            gameOver.SetActive(true);
        }
    }
}
