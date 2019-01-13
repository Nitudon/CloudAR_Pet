using System;
using UdonLib.Commons;
using UdonLib.UI;
using UnityEngine;
using CloudPet.AR;
using UnityEngine.UI;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーのUIまとめ、メンバアクセスできるFacadeカプセル的なあれ
    /// </summary>
    public class BreederUIView : UIMono, IInitializable
    {
        [SerializeField]
        private AnchorSystemUIView _anchorSystemView;
        public AnchorSystemUIView AnchorSystemUIView => _anchorSystemView;

        [SerializeField]
        private PetSystemUIView _petSystemView;
        public PetSystemUIView PetSystemUIView => _petSystemView;

        [SerializeField]
        private FadeUIGroup _warningUI;

        [SerializeField]
        private Text _warningDescription;

        public void Initialize()
        {
            SetMode(UIMode.Anchor);
        }

        public void SetMode(UIMode mode)
        {
            _anchorSystemView.SetEnable(mode == UIMode.Anchor);
            _anchorSystemView.SetAnchorSettingDescription(mode);
            _petSystemView.SetEnable(mode == UIMode.Breed);

            switch (mode)
            {
                case UIMode.None:
                case UIMode.Breed:
                    _warningUI.SetVisible(false);
                    break;
                case UIMode.Anchor:
                    _warningUI.SetVisible(true);
                    _warningDescription.text = "部屋の基準点を検出します。\n平らな所を向けて部屋に入るボタンをおしてください。";
                    break;
                case UIMode.Activate:
                    _warningUI.SetVisible(true);
                    _warningDescription.text = "ペットを呼び出す位置を検出します。\n平らな所を向けてペットを出すボタンをおしてください。";
                    break;
            }
        }
    }
}
