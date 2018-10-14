using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

public class PetPresenter : InitializableMono
{
    [SerializeField]
    private PetController _petController;

    public override void Initialize()
    {
        Bind();
    }

    private void Bind()
    {
        
    }
}
