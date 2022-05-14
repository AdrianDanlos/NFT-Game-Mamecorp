using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControlUI : MonoBehaviour
{
    Button prev;
    Button next;
    TextMeshProUGUI panelInfo;

    private void Awake()
    {
        prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        next = GameObject.Find("Button_Next").GetComponent<Button>();
        panelInfo = GameObject.Find("Button_Prev").GetComponent<TextMeshProUGUI>();
    }
}
