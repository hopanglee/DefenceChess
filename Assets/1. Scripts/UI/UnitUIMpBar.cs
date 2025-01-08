using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUIMPBar : UIBehaviour, IUIView
{
    [SerializeField]
    private Slider mpSlider;

    private int m_mp;
    public int MaxMp { private get; set; }

    public void Initialized()
    {
        mpSlider.value = 0f;
        m_mp = 0;
    }

    public void Hide()
    {
        mpSlider.gameObject.SetActive(false);
    }

    public void Show()
    {
        mpSlider.gameObject.SetActive(true);
    }

    public void MpUIUpdate(int value)
    {
        m_mp = value;
        UIUpdate();
    }

    public void MaxMpUIUpdate(int value)
    {
        MaxMp = value;
        UIUpdate();
    }

    private void UIUpdate()
    {
        mpSlider.value = (float)m_mp / MaxMp;
    }
}
