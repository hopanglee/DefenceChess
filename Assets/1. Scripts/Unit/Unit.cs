using UnityEngine;

[RequireComponent(typeof(GridPositionable))]
public abstract class Unit : MonoBehaviour
{
    // hp mp 제외한 stat
    public struct UnitStat
    {
        public int Lv;
        public int AttackPower;       // 공격력
        public int AbilityPower;      // 주문력
        public int Defense;           // 방어력
        public int MagicResistance;   // 마법 저항력
        public int AttackRange;       // 사정거리
        public int HpDrain; // 체력 흡수
        public int AttackSpeed; // 공격 속도
        public int Speed; // 이동속도
    }

    protected UnitStat unitStat;
    protected UnitState unitState;
    public bool isEnemy;

    public UnitInfo unitInfo;

    private void Awake() {
        UnitManager.AddUnit(this);
    }

    public virtual void StartTurn()
    {
        unitState = new UnitStateSearching(this);
    }

    public virtual void StopTurn()
    {
        unitState = new UnitStateIdle(this);
    }

    private void Update()
    {
        unitState.Update();
    }

    protected int CalculateDamage(AttackInfo attackInfo)
    {
        switch (attackInfo.damageType)
        {
            case AttackInfo.DamageType.True:
                return attackInfo.amount;

            case AttackInfo.DamageType.Physical:
                float physicalReducuction = (float)unitStat.Defense / (unitStat.Defense + 100);
                return Mathf.Max((int)(attackInfo.amount * (1 - physicalReducuction)), 1);

            case AttackInfo.DamageType.Magic:
                float magicalReducuction = (float)unitStat.MagicResistance / (unitStat.MagicResistance + 100);
                return Mathf.Max((int)(attackInfo.amount * (1 - magicalReducuction)), 1);

            default:
                return 0;
        }
    }

}
