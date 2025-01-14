// hp mp item
using System;
using UnityEngine;

public class Zentoo : UnitHero
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack(IHasHP target)
    {
        //AttackInfo trueAttack = new AttackInfo(AttackInfo.AttackType.Melee, AttackInfo.DamageType.True, 0);
        AttackInfo physicalAttack = new AttackInfo(
            AttackInfo.AttackType.Melee,
            AttackInfo.DamageType.Physical,
            unitStat.AttackPower
        );
        //AttackInfo magicAttack = new AttackInfo(AttackInfo.AttackType.Melee, AttackInfo.DamageType.Magic, 0);
        AttackInfo[] attackInfo = new AttackInfo[] { physicalAttack };
        Attack attack = new NormalAttack(this, attackInfo);
        target.GetAttacked(this, attack);

        AddMP(10);

        base.Attack(target);
    }

    public override void UseSkill()
    {
        //Debug.Log("SKILL !!");
        SetMP(0);
    }
}
