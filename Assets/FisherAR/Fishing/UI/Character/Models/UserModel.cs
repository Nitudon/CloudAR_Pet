using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class UserModel : UdonBehaviour
{
    [SerializeField]
    private CharacterModel _character;
    public CharacterModel Character => _character;

    private bool _isMine;
    public bool IsMine => _isMine;
}
