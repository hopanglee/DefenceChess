using System;
using UnityEngine;

public interface IHasHP
{
    bool IsDeath { get; }

    int HP { get; protected set; }
    int MaxHp { get; protected set; }

    event Action OnHPMax;
    event Action<int> OnHpUpdate;
    event Action<int> OnMaxHpUpdate;
    event Action OnHPDepleted;

    void GetAttacked(Unit attacker, Attack attack);
    bool GetAttacked(Unit attacker, AttackInfo[] attackInfo); //이번 공격으로 죽으면 true반환
    void SetHealed(int amount);

    int ShieldAmount { get; set; }
    event Action<int> OnShieldUpdate;
    event Action OnShieldDepleted;
}
