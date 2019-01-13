using System;
using UniRx;
using UdonLib.UI;
using UnityEngine;
using UnityEngine.UI;
using CloudPet.AR;
using CloudPet.Pet;

namespace CloudPet.AR
{
    /// <summary>
    /// アンカー周りのUI
    /// </summary>
    public class AnchorSystemUIView : UIMono
    {
        [SerializeField]
        private CommonButtonBase _anchorSettingButton;
        public CommonButtonBase AnchorSettingButton => _anchorSettingButton;

        [SerializeField]
        private Text _anchorSettingDescription;

        public void SetEnable(bool enable)
        {
            _anchorSettingButton.SetVisible(enable);
            _anchorSettingDescription.enabled = enable;
        }

        public void SetAnchorSettingDescription(UIMode mode)
        {
            switch (mode)
            {
                case UIMode.Anchor:
                    _anchorSettingDescription.text = "部屋に\n入る";
                    break;
                default:
                    SetEnable(false);
                    break;
            }
        }
    }
}
