using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IUIView
{
    void Initialized();
    void Show();
    void Hide();
}

public class UnitUIConnector : UIBehaviour
{
    [SerializeField]
    private UnitUIHPBar hpBar;

    [SerializeField]
    private UnitUIMPBar mpBar;

    [SerializeField]
    private UnitUIItemContainer itemContainer;

    protected override void Awake()
    {
        base.Awake();
        var _unit = FindUnitComponentInParent(transform);

        if (_unit == null)
        {
            Debug.LogError($"No Unit component found in the parent hierarchy of {gameObject.name}");
        }
        else
        {
            if (_unit is IHasHP _hpUnit)
            {
                hpBar.MaxHp = _hpUnit.MaxHp;
                hpBar.Show();

                _hpUnit.OnHpUpdate -= hpBar.HpUIUpdate;
                _hpUnit.OnHpUpdate += hpBar.HpUIUpdate;

                _hpUnit.OnMaxHpUpdate -= hpBar.MaxHpUIUpdate;
                _hpUnit.OnMaxHpUpdate += hpBar.MaxHpUIUpdate;

                _hpUnit.OnShieldUpdate -= hpBar.ShieldUIUpdate;
                _hpUnit.OnShieldUpdate += hpBar.ShieldUIUpdate;

                hpBar.Initialized();
            }
            else
            {
                hpBar.Hide();
            }

            if (_unit is IHasMP _mpUnit)
            {
                mpBar.MaxMp = _mpUnit.MaxMp;
                mpBar.Show();

                _mpUnit.OnMpUpdate -= mpBar.MpUIUpdate;
                _mpUnit.OnMpUpdate += mpBar.MpUIUpdate;

                _mpUnit.OnMaxMpUpdate -= mpBar.MaxMpUIUpdate;
                _mpUnit.OnMaxMpUpdate += mpBar.MaxMpUIUpdate;

                mpBar.Initialized();
            }
            else
            {
                mpBar.Hide();
            }

            if (_unit is IHasItem _itemUnit)
            {
                itemContainer.ItemMax = _itemUnit.ItemMax;
                itemContainer.Show();

                _itemUnit.OnUpdateItem -= itemContainer.ItemUpdate;
                _itemUnit.OnUpdateItem += itemContainer.ItemUpdate;

                itemContainer.Initialized();
            }
            else
            {
                itemContainer.Hide();
            }
        }
    }

    private Unit FindUnitComponentInParent(Transform currentTransform)
    {
        while (currentTransform != null)
        {
            var unit = currentTransform.GetComponent<Unit>();
            if (unit != null)
            {
                return unit;
            }
            currentTransform = currentTransform.parent;
        }
        return null;
    }
}
