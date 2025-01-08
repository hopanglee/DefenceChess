using System;
using UnityEngine;

public interface IHasMP
{
    bool canSkill { get; protected set; }
    int MP { get; protected set; }
    int MaxMp { get; protected set; }

    event Action OnMPMax;
    event Action<int> OnMpUpdate;
    event Action<int> OnMaxMpUpdate;
    event Action OnMPDepleted;

    void UpdateMP(int amount);
}
