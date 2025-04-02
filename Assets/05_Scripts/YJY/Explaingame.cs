using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Explaingame : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public GameObject explainimage;
    public Button button;

    private int currentStep = 0;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        explainimage.SetActive(true);
        text1.gameObject.SetActive(true);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
        text4.gameObject.SetActive(false);
    }

    void OnButtonClick()
    {
        currentStep++;

        switch (currentStep)
        {
            case 1:              
                text1.gameObject.SetActive(false);
                text2.gameObject.SetActive(true);
                break;

            case 2:
                text2.gameObject.SetActive(false);
                text3.gameObject.SetActive(true);
                break;
            case 3:
                text3.gameObject.SetActive(false);
                text4.gameObject.SetActive(true);
                break;

            case 4:
                Destroy(gameObject);
 
                break;
        }
    }
}
