using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUIHPBar : UIBehaviour, IUIView
{
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Slider shieldSlider;
    public int MaxHp { private get; set; }
    private int m_curHp;
    private int m_shieldAmount;

    public void HpUIUpdate(int value)
    {
        m_curHp = value;
        UIUpdate();
    }

    public void MaxHpUIUpdate(int value)
    {
        MaxHp = value;
        UIUpdate();
    }

    public void ShieldUIUpdate(int value)
    {
        m_shieldAmount = value;
        UIUpdate();
    }

    public void UIUpdate()
    {
        //float step;
        float hpShieldRatio;

        // 쉴드가 존재 할 때
        if (m_shieldAmount > 0)
        {
            // 현재체력 + 쉴드 > 최대 체력
            if (m_curHp + m_shieldAmount > MaxHp)
            {
                hpShieldRatio = m_curHp / (m_curHp + m_shieldAmount);
                shieldSlider.value = 1f;
                //step = (m_curHp) / steps;
                hpSlider.value = m_curHp / (m_curHp + m_shieldAmount);
            }
            else
            {
                shieldSlider.value = (m_curHp + m_shieldAmount) / MaxHp;
                hpShieldRatio = m_curHp / MaxHp;
                //step = m_curHp / steps;

                hpSlider.value = m_curHp / MaxHp;
            }

            //damaged.fillAmount = hp.fillAmount;
        }
        else
        {
            shieldSlider.value = 0f;
            //step = MaxHp / steps;
            hpShieldRatio = 1f;

            hpSlider.value = m_curHp / MaxHp;
        }

        //         damaged.fillAmount = Mathf.Lerp(damaged.fillAmount, hp.fillAmount, Time.deltaTime * speed);

        //         separator.material.SetFloat(floatSteps, step);
        //         separator.material.SetFloat(floatRatio, hpShieldRatio);
        //         separator.material.SetFloat(floatWidth, RectWidth);
        //         separator.material.SetFloat(floatThickness, Thickness);
    }

    public void Show()
    {
        hpSlider.gameObject.SetActive(true);
        shieldSlider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        hpSlider.gameObject.SetActive(false);
        shieldSlider.gameObject.SetActive(false);
    }

    public void Initialized()
    {
        hpSlider.value = 1f; // 초기화
        m_curHp = MaxHp;

        shieldSlider.value = 0f; // 초기화
    }
}
