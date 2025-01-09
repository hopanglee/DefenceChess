using UnityEngine;

[CreateAssetMenu(fileName = "UnitInfo", menuName = "Scriptable Objects/UnitInfo")]
public class UnitInfo : ScriptableObject
{
    public struct UnitBaseStat
    {
        public int MaxHp;
        public int MaxMp;
        public int MaxItem;
        public int AttackPower;       // 공격력
        public int AbilityPower;      // 주문력
        public int Defense;           // 방어력
        public int MagicResistance;   // 마법 저항력
        public int AttackRange;       // 사정거리
        public int HpDrain;
        public int Speed;
    }

    public UnitBaseStat[] LvUnitStat;
    public string Name;
    public Sprite UnitSprite;
    public int Price;
}
