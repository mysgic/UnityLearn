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
    //������Ϸ��������
    TextMeshProUGUI _tmTxtBox;
    //���ñ����洢��ǰҳ��
    int _currentPage = 1;
    int _totalPages;


    private void Start()
    {
        //��Ϸ��ʼʱ����ʾ�Ի���
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;

        //��ȡ�Ի���TMP���
        _tmTxtBox = dlgTxtGameObject.GetComponent<TextMeshProUGUI>();
    }


    //��update�н��е���ʱ
    private void Update()
    {
        //��ȡ�Ի�������У��Ի����ֵ���ҳ��
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
