using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUIItemContainer : UIBehaviour, IUIView
{
    [SerializeField]
    private GameObject _container;
    public int ItemMax { get; set; }

    public void Initialized()
    {
        throw new NotImplementedException();
    }

    public void Hide()
    {
        _container.gameObject.SetActive(false);
    }

    public void Show()
    {
        _container.gameObject.SetActive(true);
    }
}
