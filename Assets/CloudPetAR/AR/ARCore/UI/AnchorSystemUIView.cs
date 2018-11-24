using UniRx;
using UdonLib.UI;
using UnityEngine;
using UnityEngine.UI;
using CloudPet.AR;

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
            _anchorSettingButton.SetEnable(enable);
            _anchorSettingDescription.enabled = enable;
        }

        public void SetAnchorSettingDescription(string text)
        {
            _anchorSettingDescription.text = text;
        }
    }
}
