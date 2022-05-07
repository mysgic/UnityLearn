using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    public int amount=1;

    public AudioClip collectedClip;
    public float soundVolume = 1.0f;

    
    
    //��Ӵ�������ײ�¼�
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"�͵�ǰ���巢����ײ����{other}");

        //��ȡRuby��Ϸ����Ľű��������
        RubyController rubyController = other.GetComponent<RubyController>();

        if(rubyController!= null)
        {
            if(rubyController.CurrentHealth<rubyController.maxHealth)
            {
                rubyController.ChangeHealth(amount);
                //���ٵ�ǰ��Ϸ����
                Destroy(gameObject);

                //���ų�Ѫ��Ƶ����
                rubyController.PlaySound(collectedClip, soundVolume);
            }

        }
        else
        {
            Debug.LogError("rubyController��Ϸ���δ��ȡ��");
        }

    }
}
