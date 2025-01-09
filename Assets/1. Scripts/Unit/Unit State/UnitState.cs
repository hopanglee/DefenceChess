
public abstract class UnitState
{
    protected Unit m_unit;
    public UnitState(Unit unit)
    {
        this.m_unit = unit;
    }

    public virtual void Update()
    {

    }
}