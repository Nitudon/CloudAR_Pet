using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;

public class CharacterPresenter : MonoBehaviour, IInitializable
{
    private CharacterModel _model;

    private PlayableAnimationController _animationController;

    public void Initialize()
    {
        _animationController.SetAnimator(_model.CharacterAnimator);
    }
}
