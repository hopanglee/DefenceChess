using System;
using UnityEngine;

public interface IHasMP
{
    int Mp { get; set; }
    int MaxMp { get; set; }

    event Action OnMpMax;
    event Action<int> OnMpUpdate;
    event Action<int> OnMaxMpUpdate;

    event Action OnUseSkill;

    void AddMP(int amount);

    void UseSkill();
}
