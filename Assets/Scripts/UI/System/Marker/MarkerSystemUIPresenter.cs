using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.UI;
using UdonLib.Commons;

public class MarkerSystemUIPresenter : UIMono, IInitializable
{
    [SerializeField]
    private MarkerSystemUIView _view;

    public void Initialize()
    {
        
    }

    public void SetEnable(bool enable)
    {
        _view.gameObject.SetActive(enable);
    }
}
