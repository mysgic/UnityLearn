using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public ParticleSystem HitEffect;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (transform.position.magnitude > 100.0f)
            Destroy(gameObject);
    }


    public void Launch(Vector2 direction, float force)
    {
        //�ø�������AddForce��������Ϸ����ʩ����
        rigidbody2d.AddForce(direction * force);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemycontroller = other.collider.GetComponent<EnemyController>();
        if (enemycontroller != null)
            enemycontroller.Fix();

        //���ǻ������˵�����־���˽�ɵ��������Ķ���
        Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);

        Instantiate(HitEffect, transform.position, Quaternion.identity);
    }
}
