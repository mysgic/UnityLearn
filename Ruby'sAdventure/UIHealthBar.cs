using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //�������о�̬��Ա����ȡ��ǰѪ������
    public static UIHealthBar Instance { get; private set; }
    public Image mask;

    float originalSize; //���ֲ��ʼ����


    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        
    }


    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);

    }


}
