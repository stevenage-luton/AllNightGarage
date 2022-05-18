using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public static Canvas canvas;
    public static GameObject crossHairPanel, interactPanel, MouseButtons, LMBText, BasketTotalPanel;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        crossHairPanel = canvas.transform.Find("CrosshairPanel").gameObject;
        interactPanel = canvas.transform.Find("InteractingPanel").gameObject;
        MouseButtons = interactPanel.transform.Find("MouseButtons").gameObject;
        LMBText = MouseButtons.transform.Find("LMB").gameObject;
        BasketTotalPanel = interactPanel.transform.Find("BasketTotalPanel").gameObject;

        crossHairPanel.SetActive(true);
        interactPanel.SetActive(true);
        LMBText.SetActive(false);
        BasketTotalPanel.SetActive(false);
    }


    public static void ChangeUI(UIEnum uIEnum)
    {
        switch (uIEnum)
        {
            case UIEnum.CROSSHAIR:
                crossHairPanel.SetActive(true);
                break;
            case UIEnum.INTERACTING:
                crossHairPanel.SetActive(false);
                break;
            case UIEnum.MENU:
                crossHairPanel.SetActive(false);
                break;
            case UIEnum.DIALOGUE:
                crossHairPanel.SetActive(false);
                break;
        }
    }

    public static void TakeBasket()
    {
        LMBText.SetActive(true);
        BasketTotalPanel.SetActive(true);
    }
}
