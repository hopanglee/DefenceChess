using System;

public interface IHasHP
{
    bool IsDeath { get; set; }

    int Hp { get; set; }
    int MaxHp { get; set; }

    event Action OnHpMax;
    event Action<int> OnHpUpdate;
    event Action<int> OnMaxHpUpdate;
    event Action OnHpDepleted;

    void GetAttacked(Unit attacker, Attack attack);
    bool GetAttacked(Unit attacker, AttackInfo[] attackInfo); //이번 공격으로 죽으면 true반환
    void Healed(int amount);

    int ShieldAmount { get; set; }
    event Action<int> OnShieldUpdate;
    event Action OnShieldDepleted;

    void AddShield(int amount);
}
