using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public static Canvas canvas;
    public static GameObject crossHairPanel, interactPanel, MouseButtons, LMBText, BasketTotalPanel;
    public static TextMeshProUGUI LMBTMPro;
    public static TextMeshProUGUI hoverText;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        crossHairPanel = canvas.transform.Find("CrosshairPanel").gameObject;
        interactPanel = canvas.transform.Find("InteractingPanel").gameObject;
        MouseButtons = interactPanel.transform.Find("MouseButtons").gameObject;
        LMBText = MouseButtons.transform.Find("LMB").gameObject;
        BasketTotalPanel = interactPanel.transform.Find("BasketTotalPanel").gameObject;
        LMBTMPro = LMBText.GetComponent<TextMeshProUGUI>();
        hoverText = crossHairPanel.GetComponentInChildren<TextMeshProUGUI>();

        crossHairPanel.SetActive(true);
        interactPanel.SetActive(true);
        LMBText.SetActive(true);
        LMBTMPro.alpha = 0;
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
        LMBTMPro.alpha = 1;
        BasketTotalPanel.SetActive(true);
    }
    public static void DropBasket()
    {
        LMBTMPro.alpha = 0;
    }

    public static void DisplayHoverText(string text)
    {
        hoverText.alpha = 1;
        hoverText.text = text;
    }
    public static void hideHoverText()
    {
        hoverText.alpha = 0;
    }
}
