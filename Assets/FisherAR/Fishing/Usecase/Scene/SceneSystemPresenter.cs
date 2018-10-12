using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class SceneSystemPresenter : MonoBehaviour {

    [SerializeField]
    private MarkerSystemUIPresenter _markerPresenter;

    private async void Start()
    {
        _markerPresenter.Initialize();
    }
}
