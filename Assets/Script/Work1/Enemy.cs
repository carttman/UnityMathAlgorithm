using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp = 100f;
   
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
