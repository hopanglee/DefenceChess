// hp mp item
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UnitHero : Unit, IHasHP, IHasMP, IHasItem
{
    private int _hp;
    private int _maxHp;
    private int _mp;
    private int _maxMp;
    private int _shieldAmount;


    public int ShieldAmount
    {
        get => _shieldAmount;
        set
        {
            int new_amount = Mathf.Max(value, 0);
            if (_shieldAmount != new_amount)
            {
                _shieldAmount = new_amount;
                OnShieldUpdate?.Invoke(new_amount);

                if (new_amount == 0)
                    OnShieldDepleted?.Invoke();

            }
        }
    }
    public bool IsDeath { get; set; }
    public int Hp
    {
        get => _hp;
        set
        {
            int new_hp = Mathf.Clamp(value, 0, MaxHp);
            if (_hp != new_hp)
            {
                _hp = new_hp;
                OnHpUpdate?.Invoke(new_hp);

                if (new_hp == 0)
                    OnHpDepleted?.Invoke();

                else if (new_hp == MaxHp)
                    OnHpMax?.Invoke();

            }
        }
    }
    public int MaxHp
    {
        get => _maxHp;
        set
        {
            int new_amount = Mathf.Max(value, 0);
            if (_maxHp != new_amount)
            {
                _maxHp = new_amount;
                OnMaxHpUpdate?.Invoke(new_amount);
            }
        }
    }

    public int Mp
    {
        get => _mp;
        set
        {
            int new_mp = Mathf.Clamp(value, 0, MaxMp);
            if (_mp != new_mp)
            {
                _mp = new_mp;
                OnMpUpdate?.Invoke(new_mp);

                if (new_mp == MaxMp)
                    OnMpMax?.Invoke();

            }
        }
    }

    public int MaxMp
    {
        get => _maxMp;
        set
        {
            int new_amount = Mathf.Max(value, 0);
            if (_maxMp != new_amount)
            {
                _maxMp = new_amount;
                OnMaxMpUpdate?.Invoke(new_amount);
            }
        }
    }

    public int ItemMax { get; set; }
    public List<Item> Items { get; set; }

    public event Action OnHpMax;
    public event Action<int> OnHpUpdate;
    public event Action<int> OnMaxHpUpdate;
    public event Action OnHpDepleted;
    public event Action<int> OnShieldUpdate;
    public event Action OnShieldDepleted;

    public event Action OnGetAttacked;

    public event Action OnMpMax;

    public event Action<int> OnMpUpdate;

    public event Action<int> OnMaxMpUpdate;

    public event Action OnUseSkill;
    public event Action<List<Item>> OnUpdateItem;

    public event Action OnAttack;

    private void OnEnable()
    {

        OnHpDepleted += () =>
        {
            // 나중에 부활아이템 생기면 조건 추가 
            IsDeath = true;
        };

        OnUpdateItem += (_) =>
        {
            ReloadStat();
        };

        OnMpMax += () => UseSkill();
    }

    private void OnDisable()
    {

        OnHpDepleted -= () =>
        {
            // 나중에 부활아이템 생기면 조건 추가 
            IsDeath = true;
        };

        OnUpdateItem -= (_) =>
        {
            ReloadStat();
        };

        OnMpMax -= () => UseSkill();
    }

    public void GetAttacked(Unit attacker, Attack attack)
    {
        if (attacker.isEnemy != this.isEnemy)
        {
            attack.StartAttack(this);
        }

    }

    public virtual bool GetAttacked(Unit attacker, AttackInfo[] attackInfo)
    {
        foreach (var _attackInfo in attackInfo)
        {
            var damage = CalculateDamage(_attackInfo);

            // 이후 데미지 텍스트 UI관련
            switch (_attackInfo.damageType)
            {
                case AttackInfo.DamageType.True:

                    break;

                case AttackInfo.DamageType.Physical:

                    break;

                case AttackInfo.DamageType.Magic:

                    break;
            }

            if (ShieldAmount > damage)
            {
                ShieldAmount -= damage;
            }
            else if (ShieldAmount > 0)
            {
                var temp = ShieldAmount;
                ShieldAmount = 0;
                Hp -= damage - temp;
            }
            else Hp -= damage;

            if (Hp <= 0)
            {
                break;
            }
        }

        OnGetAttacked?.Invoke();

        if (Hp <= 0) return true;

        return false;
    }

    public virtual void Healed(int amount)
    {
        Hp += amount;
    }

    public virtual void AddShield(int amount)
    {
        ShieldAmount += amount;
    }

    public virtual void AddMP(int amount)
    {
        Mp += amount;
    }

    public bool AddItem(Item item)
    {
        if (Items.Count < ItemMax)
        {
            Items.Add(item);
            OnUpdateItem?.Invoke(Items);
            return true;
        }

        return false;
    }
    public void RemoveItemAll()
    {
        Items.Clear();
        OnUpdateItem?.Invoke(Items);
    }

    public abstract void UseSkill();
    public abstract void Attack();

    public override void StartTurn()
    {
        ReloadStat();
        base.StartTurn();
        
    }
    private void ReloadStat()
    {
        MaxHp = unitInfo.LvUnitStat[unitStat.Lv - 1].MaxHp + Items.Sum(item => item.itemStat.MaxHp);
        Hp = MaxHp;

        MaxMp = unitInfo.LvUnitStat[unitStat.Lv - 1].MaxMp + Items.Sum(item => item.itemStat.MaxMp);
        Mp = Items.Sum(item => item.itemStat.BaseMp);

        unitStat.AttackPower = unitInfo.LvUnitStat[unitStat.Lv - 1].AttackPower + Items.Sum(item => item.itemStat.AttackPower);       // 공격력

        unitStat.AbilityPower = unitInfo.LvUnitStat[unitStat.Lv - 1].AbilityPower + Items.Sum(item => item.itemStat.AbilityPower);      // 주문력

        unitStat.Defense = unitInfo.LvUnitStat[unitStat.Lv - 1].Defense + Items.Sum(item => item.itemStat.Defense);           // 방어력

        unitStat.MagicResistance = unitInfo.LvUnitStat[unitStat.Lv - 1].MagicResistance + Items.Sum(item => item.itemStat.MagicResistance);   // 마법 저항력

        unitStat.AttackRange = unitInfo.LvUnitStat[unitStat.Lv - 1].AttackRange + Items.Sum(item => item.itemStat.AttackRange);       // 사정거리

        unitStat.AttackSpeed = unitInfo.LvUnitStat[unitStat.Lv - 1].AttackSpeed + Items.Sum(item => item.itemStat.AttackSpeed); // 공격 속도

        unitStat.HpDrain = unitInfo.LvUnitStat[unitStat.Lv - 1].HpDrain + Items.Sum(item => item.itemStat.HpDrain); // 체력 흡수

        unitStat.Speed = unitInfo.LvUnitStat[unitStat.Lv - 1].Speed;

    }
}
