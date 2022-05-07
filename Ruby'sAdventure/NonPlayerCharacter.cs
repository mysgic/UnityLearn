using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;

    public GameObject dlgTxtGameObject;
    //创建游戏组件类对象
    TextMeshProUGUI _tmTxtBox;
    //设置变量存储当前页数
    int _currentPage = 1;
    int _totalPages;


    private void Start()
    {
        //游戏开始时不显示对话框
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;

        //获取对话框TMP组件
        _tmTxtBox = dlgTxtGameObject.GetComponent<TextMeshProUGUI>();
    }


    //在update中进行倒计时
    private void Update()
    {
        //获取对话框组件中，对话文字的总页数
        _totalPages = _tmTxtBox.textInfo.pageCount;

        if (timerDisplay >= 0.0f)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (_currentPage < _totalPages)
                {
                    _currentPage++;
                }
                else
                {
                    _currentPage = 1;
                }
                _tmTxtBox.pageToDisplay = _currentPage;
            }


            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
                dialogBox.SetActive(false);
        }


        
    }


    public void DisplayDialog()
    {
        timerDisplay = displayTime;

        dialogBox.SetActive(true);

    }

}
