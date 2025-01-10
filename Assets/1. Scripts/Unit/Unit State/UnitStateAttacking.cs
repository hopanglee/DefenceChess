using UnityEngine;

public class UnitStateAttacking : UnitState
{
    private IHasHP m_target;
    private float m_timer = 0;

    public UnitStateAttacking(Unit unit, IHasHP target) : base(unit)
    {
        this.m_target = target;
        m_timer = 0;
    }



    public override void Update()
    {
        Debug.Log($"{m_unit.name} > Attacking");

        base.Update();

        if (m_target.IsDeath) // 타겟이 죽었으면 다른 타켓팅으로 바꿈
        {
            m_unit.ChangeState(new UnitStateSearching(m_unit)); // 다시 가장 가까운 적 타겟팅하기위해 바꿈
        }

        var attackRange = m_unit.unitStat.AttackRange;
        var distance = Node.GetNodeDistance(((Unit)m_target).GetPosition(), m_unit.GetPosition());
        if (distance > attackRange)
        {
            // 타겟팅이 사거리를 벗어남
            m_unit.ChangeState(new UnitStateSearching(m_unit)); // 다시 가장 가까운 적 타겟팅하기위해 바꿈
            return;
        }

        // 공격코루틴 시작
        CheckAttackTimer();
    }

    private void CheckAttackTimer()
    {
        var attackSpeed = m_unit.unitStat.AttackSpeed;
        if (m_timer < 1f / attackSpeed)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0;

            // 공격
            m_unit.Attack((IHasHP)m_target);
        }
    }

}