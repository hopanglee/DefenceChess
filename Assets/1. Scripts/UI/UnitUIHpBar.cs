using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUIHPBar : UIBehaviour, IUIView
{
    #region Separator Shader Field
    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);
    #endregion


    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Slider shieldSlider;
    public int MaxHp;
    [SerializeField]
    private int m_curHp;
    [SerializeField]
    private int m_shieldAmount;

    [SerializeField]
    private Image separator;

    private float steps = 10f;

    //public float RectWidth = 100f;

    [Range(0, 5f)]
    [SerializeField]
    private float Thickness = 2f;

    [ContextMenu("Create Material")]
    private void CreateMaterial()
    {
        {
            separator.material = new Material(Shader.Find("UI/Health Separator"));
        }
    }

    protected override void Awake()
    {
        CreateMaterial();
    }

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
        float step;
        //float hpShieldRatio;

        // 쉴드가 존재 할 때
        if (m_shieldAmount > 0)
        {
            // 현재체력 + 쉴드 > 최대 체력
            if (m_curHp + m_shieldAmount > MaxHp)
            {
                //hpShieldRatio = (float)m_curHp / (m_curHp + m_shieldAmount);
                shieldSlider.value = 1f;
                //step = m_curHp / steps;
                step = (MaxHp + m_shieldAmount) / steps;
                hpSlider.value = (float)m_curHp / (m_curHp + m_shieldAmount);
            }
            else
            {
                shieldSlider.value = (float)(m_curHp + m_shieldAmount) / MaxHp;
                //hpShieldRatio = (float)m_curHp / MaxHp;
                //step = m_curHp / steps;
                step = (MaxHp + m_shieldAmount) / steps;

                hpSlider.value = (float)m_curHp / MaxHp;
            }

            //damaged.fillAmount = hp.fillAmount;
        }
        else
        {
            shieldSlider.value = 0f;
            step = MaxHp / steps;
            //hpShieldRatio = 1f;

            hpSlider.value = (float)m_curHp / MaxHp;
        }

        //         damaged.fillAmount = Mathf.Lerp(damaged.fillAmount, hp.fillAmount, Time.deltaTime * speed);

        separator.material.SetFloat(floatSteps, step);
        //separatorMat.SetFloat(floatRatio, hpShieldRatio);
        separator.material.SetFloat(floatRatio, 1f);
        //separatorMat.SetFloat(floatWidth, RectWidth);
        separator.material.SetFloat(floatThickness, Thickness);
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
