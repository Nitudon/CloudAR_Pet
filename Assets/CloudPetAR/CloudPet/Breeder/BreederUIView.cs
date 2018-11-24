using UniRx;
using UdonLib.UI;
using UnityEngine;
using CloudPet.AR;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのUIまとめ、メンバアクセスできるFacadeカプセル的なあれ
    /// </summary>
    public class BreederUIView : UIMono
    {
        [SerializeField]
        private AnchorSystemUIView _anchorSystemView;
        public AnchorSystemUIView AnchorSystemUIView => _anchorSystemView;

        [SerializeField]
        private PetSystemUIView _petSystemView;
        public PetSystemUIView PetSystemUIView => _petSystemView;

        [SerializeField]
        private FadeUIGroup _waitingUI;

        public void SetMode(UIMode mode)
        {
            _waitingUI.FadeGroup(mode == UIMode.Wait);
            _anchorSystemView.SetEnable(mode == UIMode.Anchor);
            _petSystemView.SetEnable(mode == UIMode.Breed);
        }
    }
}
