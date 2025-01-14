using UnityEngine;

public class UnitStateSkill : UnitState
{
    public UnitStateSkill(Unit unit)
        : base((Unit)unit)
    {
        m_unit.animator.SetBool("IsSkill", true);
    }

    public override void Update()
    {
        //Debug.Log($"{m_unit.name} > Skill");

        AnimatorStateInfo stateInfo = m_unit.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Skill") && stateInfo.normalizedTime >= 1.0f)
        {
            ((IHasMP)m_unit).UseSkill(); // 애니메이션 종료 시 실행
            m_unit.animator.SetBool("IsSkill", false);
            m_unit.ChangeState(new UnitStateSearching(m_unit));
        }

        base.Update();
    }

}
