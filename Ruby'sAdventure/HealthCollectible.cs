using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    public int amount=1;

    public AudioClip collectedClip;
    public float soundVolume = 1.0f;

    
    
    //添加触发器碰撞事件
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"和当前物体发生碰撞的是{other}");

        //获取Ruby游戏对象的脚本组件对象
        RubyController rubyController = other.GetComponent<RubyController>();

        if(rubyController!= null)
        {
            if(rubyController.CurrentHealth<rubyController.maxHealth)
            {
                rubyController.ChangeHealth(amount);
                //销毁当前游戏对象
                Destroy(gameObject);

                //播放吃血音频剪辑
                rubyController.PlaySound(collectedClip, soundVolume);
            }

        }
        else
        {
            Debug.LogError("rubyController游戏组件未获取到");
        }

    }
}
