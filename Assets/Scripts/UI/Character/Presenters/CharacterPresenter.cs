using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;

public class CharacterPresenter : MonoBehaviour, IAsyncInitializable
{
    private CharacterModel _model;

    private PlayableAnimationController _animationController;

    public async Task Initialize()
    {
        _animationController.SetAnimator(_model.CharacterAnimator);
    }
}
