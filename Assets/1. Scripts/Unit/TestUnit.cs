using System;
using UnityEngine;

public class TestUnit : Unit, IHasHP, IHasMP
{
    public bool IsDeath => false;

    public int ShieldAmount
    {
        get => 0;
        set { }
    }

    int IHasHP.HP { get; set; }
    int IHasHP.MaxHp
    {
        get => 100;
        set { }
    }
    int IHasHP.ShieldAmount
    {
        get => 0;
        set { }
    }

    bool IHasMP.canSkill
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    int IHasMP.MP
    {
        get => 0;
        set { }
    }
    int IHasMP.MaxMp
    {
        get => 100;
        set { }
    }

    public event Action OnHPMax;
    public event Action<int> OnHpUpdate;
    public event Action<int> OnMaxHpUpdate;
    public event Action OnHPDepleted;
    public event Action<int> OnShieldUpdate;
    public event Action OnShieldDepleted;
    public event Action OnMPMax;
    public event Action<int> OnMpUpdate;
    public event Action<int> OnMaxMpUpdate;
    public event Action OnMPDepleted;

    public void GetAttacked(Unit attacker, Attack attack)
    {
        // 빈 메서드
    }

    public bool GetAttacked(Unit attacker, AttackInfo[] attackInfo)
    {
        return false; // 기본 반환값
    }

    public void SetHealed(int amount)
    {
        // 빈 메서드
    }

    public void UpdateMP(int amount)
    {
        throw new NotImplementedException();
    }

    void IHasMP.UpdateMP(int amount)
    {
        throw new NotImplementedException();
    }
}
