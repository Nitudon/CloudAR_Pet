using UniRx;
using UdonLib.UI;
using UnityEngine;
using UnityEngine.UI;
using CloudPet.AR;

namespace CloudPet.Pet
{
    /// <summary>
    /// ペット周りのUI
    /// </summary>
    public class PetSystemUIView : UIMono
    {
        [SerializeField]
        private CommonButtonBase _petActivationButton;
        public CommonButtonBase PetActivationButton => _petActivationButton;

        public void SetEnable(bool enable)
        {
            _petActivationButton.SetEnable(enable);
        }
    }
}
