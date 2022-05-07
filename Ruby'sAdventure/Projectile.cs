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
        //用刚体对象的AddForce方法对游戏对象施加力
        rigidbody2d.AddForce(direction * force);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemycontroller = other.collider.GetComponent<EnemyController>();
        if (enemycontroller != null)
            enemycontroller.Fix();

        //我们还增加了调试日志来了解飞弹触碰到的对象
        Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);

        Instantiate(HitEffect, transform.position, Quaternion.identity);
    }
}
