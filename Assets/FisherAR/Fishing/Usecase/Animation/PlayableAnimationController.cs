using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UdonLib.Commons;

public class PlayableAnimationController : UdonBehaviour, IInitializable
{
    private SimpleAnimation _playableAnimator;

    public void Initialize()
    {
        _playableAnimator = new SimpleAnimation();
    }

    public void SetAnimator(Animator animator)
    {
        _playableAnimator.SetAnimator(animator);
    }

    public void PlayAnimation(bool isLoop)
    {

    }
}
