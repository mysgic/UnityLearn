using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    // true时垂直移动，false水平移动
    public bool vertical;
    Rigidbody2D rigidbody2d;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;


    Animator animator;

    bool broked = true;

    //开放一个属性，用于获取烟雾对象
    public ParticleSystem smoke;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broked)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broked)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
            
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
            

        rigidbody2d.MovePosition(position);

    }



    private void OnCollisionEnter2D (Collision2D other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();

        if(rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }



    public void Fix()
    {
        broked = false;

        //取消刚体，让机器人不再被碰撞
        rigidbody2d.simulated = false;

        //播放修好后动画
        animator.SetTrigger("Fixed");

        //Destroy(smoke);  立即完全消失
        smoke.Stop(); //停止特效产生新的粒子，原有粒子走完生命周期

        GetComponent<AudioSource>().Stop();

    }

}
