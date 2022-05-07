using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    // trueʱ��ֱ�ƶ���falseˮƽ�ƶ�
    public bool vertical;
    Rigidbody2D rigidbody2d;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;


    Animator animator;

    bool broked = true;

    //����һ�����ԣ����ڻ�ȡ�������
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

        //ȡ�����壬�û����˲��ٱ���ײ
        rigidbody2d.simulated = false;

        //�����޺ú󶯻�
        animator.SetTrigger("Fixed");

        //Destroy(smoke);  ������ȫ��ʧ
        smoke.Stop(); //ֹͣ��Ч�����µ����ӣ�ԭ������������������

        GetComponent<AudioSource>().Stop();

    }

}
