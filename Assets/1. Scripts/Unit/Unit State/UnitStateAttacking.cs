using UnityEngine;

public class UnitStateAttacking : UnitState
{
    private IHasHP m_target;
    private float m_timer = 0;

    public UnitStateAttacking(Unit unit, IHasHP target)
        : base(unit)
    {
        this.m_target = target;
        m_timer = 0;

        m_unit.animator.speed = m_unit.unitStat.AttackSpeed;
        //m_unit.animator.SetFloat("AttackSpeed", 1 / m_unit.unitStat.AttackSpeed);
    }

    public override void Update()
    {
        //Debug.Log($"{m_unit.name} > Attacking");

        base.Update();

        if (m_target.IsDeath) // 타겟이 죽었으면 다른 타켓팅으로 바꿈
        {
            m_unit.animator.SetBool("IsAttacking", false);
            m_unit.ChangeState(new UnitStateSearching(m_unit)); // 다시 가장 가까운 적 타겟팅하기위해 바꿈
            return;
        }

        var attackRange = m_unit.unitStat.AttackRange;
        var distance = Node.GetNodeDistance(((Unit)m_target).GetPosition(), m_unit.GetPosition());
        if (distance > attackRange)
        {
            // 타겟팅이 사거리를 벗어남
            m_unit.animator.SetBool("IsAttacking", false);

            m_unit.ChangeState(new UnitStateSearching(m_unit)); // 다시 가장 가까운 적 타겟팅하기위해 바꿈
            return;
        }

        // 바라볼 방향 계산
        Vector3 direction = (
            ((Unit)m_target).transform.position - m_unit.transform.position
        ).normalized;
        // 방향 회전
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            m_unit.transform.rotation = Quaternion.Lerp(
                m_unit.transform.rotation,
                targetRotation,
                Time.deltaTime * 30f
            ); // 부드러운 회전
        }

        if (!m_unit.animator.GetBool("IsAttacking")) // 새로운 공격루틴 시작
        {
            m_unit.animator.SetBool("IsAttacking", true);
        }
        // 공격코루틴 시작
        CheckAttackTimer();
    }

    private void CheckAttackTimer()
    {

        var attackSpeed = m_unit.unitStat.AttackSpeed;

        //Debug.Log($"Attack중 {m_timer} / {1f / attackSpeed}");
        if (m_timer < 1f / attackSpeed)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0;
            Debug.Log("ATTACK");
            // 공격
            m_unit.Attack((IHasHP)m_target);
            m_unit.animator.SetBool("IsAttacking", false); // 공격한턴 끝
        }
    }
}
