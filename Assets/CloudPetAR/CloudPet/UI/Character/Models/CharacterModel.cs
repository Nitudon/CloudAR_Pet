using System;
using UnityEngine;
using UdonLib.Commons;
using UniRx.Async;
using UniRx.Triggers;

public class CharacterModel : UdonBehaviour, IAsyncInitializable
{
    [SerializeField]
    private Collider _characterCollider;

    [SerializeField]
    private Collider _searchCollider;

    [SerializeField]
    private Animator _characterAnimator;
    public Animator CharacterAnimator => _characterAnimator;

    private string _characterName;

    private IObservable<Collider> _searchObservable;
    public IObservable<Collider> SearchObservable => _searchObservable;

    public async UniTask Initialize()
    {
        _searchObservable = _searchCollider.OnTriggerEnterAsObservable();
    }
}
