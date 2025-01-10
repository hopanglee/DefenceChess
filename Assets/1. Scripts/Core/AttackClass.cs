using System;
using System.Collections;
using UnityEngine;

public class AttackInfo
{
    public enum AttackType
    {
        Melee,
        Ranged,
    }

    public AttackType attackType;

    public enum DamageType
    {
        Physical,
        Magic,
        True,
    }

    public DamageType damageType;
    public int amount;

    public AttackInfo(AttackType attackType, DamageType damageType, int amount)
    {
        this.attackType = attackType;
        this.damageType = damageType;
        this.amount = amount;
    }
}

public abstract class Attack
{
    protected Unit m_attacker;
    protected AttackInfo[] m_info;
    protected Action<IHasHP> m_onKilled;

    public Attack(Unit attacker, AttackInfo[] info, Action<IHasHP> onKilled)
    {
        m_attacker = attacker;
        m_info = info;
        m_onKilled += onKilled;
    }

    public abstract void StartAttack(IHasHP hpUnit);
    protected abstract void StopAttack();
}

public class NormalAttack : Attack
{
    public NormalAttack(Unit attacker, AttackInfo[] attackInfo, Action<IHasHP> onKilled = null)
        : base(attacker, attackInfo, onKilled) { }

    public override void StartAttack(IHasHP target)
    {
        target.OnHpDepleted += StopAttack;
        bool isKilled = target.GetAttacked(m_attacker, m_info); // 즉시 피해

        if (isKilled)
        {
            m_onKilled?.Invoke(target);
        }
    }

    protected override void StopAttack()
    {
        return;
    }
}

public class DelayAttack : Attack
{
    private Coroutine m_currentCoroutine; // 현재 실행 중인 코루틴 저장
    private float m_delay;

    public DelayAttack(Unit attacker, AttackInfo[] attackInfo, float delay, Action<IHasHP> onKilled)
        : base(attacker, attackInfo, onKilled)
    {
        m_delay = delay;
    }

    public override void StartAttack(IHasHP target)
    {
        target.OnHpDepleted += StopAttack;
        m_currentCoroutine = CoroutineRunner.Instance.RunCoroutine(WaitCoroutine(target));
    }

    protected override void StopAttack()
    {
        if (m_currentCoroutine != null)
        {
            CoroutineRunner.Instance.RemoveCoroutine(m_currentCoroutine);
            m_currentCoroutine = null; // 코루틴 객체 해제
        }
    }

    IEnumerator WaitCoroutine(IHasHP target)
    {
        yield return new WaitForSeconds(m_delay);
        if (!target.IsDeath)
        {
            bool isKilled = target.GetAttacked(m_attacker, m_info);

            if (isKilled)
            {
                m_onKilled?.Invoke(target);
            }
        }
    }
}

public class PoisonAttack : Attack
{
    private Coroutine m_currentCoroutine; // 현재 실행 중인 코루틴 저장
    private float m_duration;

    public PoisonAttack(
        Unit attacker,
        AttackInfo[] attackInfo,
        float duration,
        Action<IHasHP> onKilled
    )
        : base(attacker, attackInfo, onKilled)
    {
        m_duration = duration;
    }

    public override void StartAttack(IHasHP target)
    {
        target.OnHpDepleted += StopAttack;
        m_currentCoroutine = CoroutineRunner.Instance.RunCoroutine(RepeatCoroutine(target));
    }

    protected override void StopAttack()
    {
        if (m_currentCoroutine != null)
        {
            CoroutineRunner.Instance.RemoveCoroutine(m_currentCoroutine);
            m_currentCoroutine = null; // 코루틴 객체 해제
        }
    }

    IEnumerator RepeatCoroutine(IHasHP target)
    {
        float interval = 0.5f;
        // 데미지를 나누어 설정
        foreach (var _i in m_info)
        {
            _i.amount = Mathf.Max(1, (int)(_i.amount / (m_duration / interval))); // interval에 맞춰 데미지 조정
        }

        // 반복 공격
        while (m_duration > 0)
        {
            if (!target.IsDeath)
            {
                bool isKilled = target.GetAttacked(m_attacker, m_info);

                if (isKilled)
                {
                    m_onKilled?.Invoke(target);
                    yield break; // 대상이 죽으면 코루틴 종료
                }
            }

            yield return new WaitForSeconds(interval); // 사용자 지정 간격
            m_duration -= interval; // 지속 시간 감소
        }
    }
}
