using System;
using UnityEngine;
using UdonLib.UI;
using UnityEngine.EventSystems;
using UniRx;

public class TapDetector : UIMono, IPointerDownHandler
{
    public Subject<PointerEventData> TapEvent{ get; private set; }

    [SerializeField]
    private HitRectArea _tapArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(TapEvent == null)
        {
            TapEvent = new Subject<PointerEventData>().AddTo(gameObject);
        }

        TapEvent.OnNext(eventData);
    }
}