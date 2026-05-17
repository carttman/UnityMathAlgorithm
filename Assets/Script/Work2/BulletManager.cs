using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bullet;

    private GameObject[] bullets = new GameObject[13];
    private bool[] moving;
    private bool[] returning;

    private float angle = 30f;
    private float speed = 10f;
    private float returnSpeed = 10f;
    private float fireInterval = 0.1f;
    private float lifeTime = 1f;
    private float speedIncrement = 4f; // 사이클마다 증가량

    private void Start()
    {
        moving = new bool[bullets.Length];
        returning = new bool[bullets.Length];

        for (int i = 0; i < bullets.Length; i++)
            bullets[i] = Instantiate(bullet, transform.position, transform.rotation);

        StartCoroutine(Loop()); // 발사-복귀 반복
    }

    private void Update()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (returning[i])
            { // 각 탄 원 위치로
                bullets[i].transform.position = Vector3.MoveTowards(
                    bullets[i].transform.position,
                    transform.position,
                    returnSpeed * Time.deltaTime);
            }
            else if (moving[i])
            { // 각 탄 앞으로
                bullets[i].transform.position += bullets[i].transform.right * (speed * Time.deltaTime);
            }
        }
    }

    IEnumerator Loop()
    {
        while (true)
        {
            yield return StartCoroutine(Fire());
            yield return new WaitUntil(AllReturned); // 모든 탄 복귀 대기

            for (int i = 0; i < bullets.Length; i++)
            {
                moving[i] = false;
                returning[i] = false;
            }
            speed += speedIncrement; // 다음 사이클 속도 증가
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            float z = (i * angle) % 360; // 각 탄 각도 설정
            bullets[i].transform.rotation = Quaternion.Euler(0, 0, z);
            moving[i] = true;
            StartCoroutine(ReturnAfter(i, lifeTime));

            yield return new WaitForSeconds(fireInterval);
        }
    }
    //
    IEnumerator ReturnAfter(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        returning[index] = true;
    }

    bool AllReturned()
    {   // 전부 돌아왔는지 검사
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!returning[i]) return false;
            if (Vector3.Distance(bullets[i].transform.position, transform.position) > 0.01f)
                return false;
        }
        returnSpeed += speedIncrement;
        return true;
    }
}
