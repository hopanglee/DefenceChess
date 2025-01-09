public class UnitStateSearching : UnitState
{
    GridMovable gridMovable;
    public UnitStateSearching(Unit unit) : base(unit)
    {
        this.gridMovable = unit.GetComponent<GridMovable>();
    }

    public override void Update()
    {
        base.Update();

        // 사거리 밖에 적 존재
        // 가장 가까운 적에게 이동

        if (gridMovable && gridMovable.isMoving) // 이동 중이면 패스
        {
            return; 
        }

        var nearest = UnitManager.GetNearestEnemy(m_unit.isEnemy, m_unit.transform.position);

        if (nearest != null && nearest != m_unit)
        {
            var distance = Node.GetNodeDistance(nearest.GetPosition(), m_unit.GetPosition());

            if (distance <= m_unit.unitStat.AttackRange)
            {
                // 사거리 안에 적 존재
                m_unit.ChangeState(new UnitStateAttacking(m_unit, nearest));
                return;
            }
            else
            {
                // 사거리 밖에 적 존재
                // 가장 가까운 적에게 이동
                if (gridMovable) // 이동 가능
                {
                    gridMovable.MoveTo(nearest, m_unit.unitStat.Speed);
                }
            }
        }

        // 적 존재 x

    }
}