using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        //  set { currentHealth = value; }
    }

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;



    float horizontal;
    float vertical;

    Rigidbody2D rigidbody2d;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);


    public GameObject projectilePrefab;


    //声明音频源对象，用于后期音频播放控制
    AudioSource audioSource;



    public AudioClip throwCogClip;
    public float throwCogSoundVol = 1.0f;

    public AudioClip playerHitClip;
    public float playerHitSoundVol = 1.0f;


    // 在第一次帧更新之前调用 Start
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();


        audioSource = GetComponent<AudioSource>();



    }
    // 每帧调用一次 Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        if(!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize(); //归一化用于存储方向的向量
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if(isInvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;

            if(invincibleTimer<=0)
            isInvincible = false; 
        }


        //添加发射子弹的逻辑
        if (Input.GetKeyDown(KeyCode.C)|| Input.GetAxis("Fire1")!=0) { Launch(); }


        //激活射线投射
        if (Input.GetKeyDown(KeyCode.X))
        {
            //创建一个射线投射碰撞对象，来接收射线投射碰撞信息
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider !=null)
            {
                Debug.Log($"射线投射碰撞对象是：{hit.collider.gameObject}");
                
                //创建npc代码组件
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if(npc!=null)
                {
                    npc.DisplayDialog();
                }
            }
        }


    }


    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.position = position;
    }


    public void ChangeHealth(int amount)
    {
        if(amount<0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            PlaySound(playerHitClip, playerHitSoundVol);
        }



        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);

    }


    void Launch()
    {
        //创建子弹游戏对象
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        //获取子弹游戏对象的脚本组件对象
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        //通过脚本对象调用子弹移动的方法
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwCogClip, throwCogSoundVol);
    }

    //播放音频剪辑的方法
    public void PlaySound(AudioClip audioClip, float soundVolume)
    {
        audioSource.PlayOneShot(audioClip, soundVolume);
    }


}
