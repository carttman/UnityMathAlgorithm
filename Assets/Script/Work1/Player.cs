using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    public Transform targetTrs;
    public GameObject bullet;
    void Start()
    {
        
    }

    void Update()
    {
        Move();
        
        if(Input.GetMouseButtonDown(0))
            Attack();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        Quaternion randomRot = Random.rotation;
        Instantiate(bullet, transform.position, randomRot);
    }
}
