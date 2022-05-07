using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //创建公有静态成员，获取当前血条本身
    public static UIHealthBar Instance { get; private set; }
    public Image mask;

    float originalSize; //遮罩层初始长度


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
