public class UnitStateSearching : UnitState
{
    GridMovable gridMovable;
    public UnitStateSearching(Unit unit) : base(unit)
    {
        this.gridMovable = unit.GetComponent<GridMovable>();
    }

    public override void Update()
    {
        var nearest = UnitManager.GetNearestEnemy(m_unit.isEnemy, m_unit.transform.position);


    }
}