﻿using System;
using UniRx;
using UdonLib.Commons;
using UnityEngine;
using CloudPet.AR;
using CloudPet.Network;

namespace CloudPet.Pet
{
    /// <summary>
    /// ブリーダーの各ロジックのバインド
    /// </summary>
    public class BreederPresenter : InitializableMono, IDisposable
    {
        [SerializeField]
        private PlaneDetectionGesture _planeDetectionGesture;

        [SerializeField]
        private CloudAnchorSystem _cloudAnchorSystem;

        [SerializeField]
        private Transform _petRoot;

        [SerializeField]
        private BreederUIView _breederUIView;

        private BreederModel _model;
        public BreederModel Model => _model;

        private BreederActivatorUseCase _activatorUseCase;
        private BreederARUseCase _arUseCase;

        /// <summary>
        /// 現在出してるペット
        /// </summary>
        private PetPresenter _petPresenter;
        public PetPresenter PetPresenter => _petPresenter;

        public PetInfo OwnPet => _petPresenter.Model.Info.Value;

        private IDisposable _automaticARDisposable;
        private IDisposable _manualARDisposable;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public override void Initialize()
        {
            _model = new BreederModel();
            _breederUIView.Initialize();

            _activatorUseCase = new BreederActivatorUseCase(_model, _cloudAnchorSystem);
            _arUseCase = new BreederARUseCase(_planeDetectionGesture, _cloudAnchorSystem.AnchorModel);

            if (PhotonNetwork.isNonMasterClientInRoom)
            {
                _cloudAnchorSystem.SetResolverMode();
            }
            else
            {
                _cloudAnchorSystem.SetHostMode();
            }

            BindCommon();
            BindAutomaticTrackingUseCase();
            SetEvent();

            _model.SetMode(UIMode.Anchor);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void BindCommon()
        {
            _model
                .Mode
                .Where(mode => mode != UIMode.None)
                .Subscribe(_breederUIView.SetMode)
                .AddTo(gameObject)
                .AddTo(_disposable);

            _model
                .OnActivatePet
                .Subscribe(info => _activatorUseCase.ActivatePet(_petRoot, info))
                .AddTo(gameObject)
                .AddTo(_disposable);
        }

        private void BindAutomaticTrackingUseCase()
        {
            _automaticARDisposable =
                _arUseCase
                    .AutomaticCenterTrackingHitInfo
                    .Where(_ => _model.Mode.Value == UIMode.Anchor)
                    .Subscribe(info =>
                    {
                      _breederUIView.AnchorSystemUIView.AnchorSettingButton.SetEnable(info.Item1);

                        if (info.Item1)
                        {
                            _cloudAnchorSystem.AnchorModel.SetPlacedAnchorRoot(info.Item1, info.Item2);
                        }
                    })
                    .AddTo(gameObject)
                    .AddTo(_disposable);
        }

        private void BindManualTrackingUseCase()
        {
            _automaticARDisposable =
                _arUseCase
                    .ManualTouchTrackingHitInfo
                    .Where(_ => _model.Mode.Value == UIMode.Anchor)
                    .Subscribe(info =>
                    {
                        _breederUIView.AnchorSystemUIView.AnchorSettingButton.SetEnable(info.Item1);

                        if (info.Item1)
                        {
                            _cloudAnchorSystem.AnchorModel.SetPlacedAnchorRoot(info.Item1, info.Item2);
                        }
                    })
                    .AddTo(gameObject)
                    .AddTo(_disposable);
        }

        private void SetEvent()
        {
            _breederUIView.AnchorSystemUIView.AnchorSettingButton.onClickedCallback += () =>
            {
                if (!_cloudAnchorSystem.AnchorModel.IsTrackable.Value)
                {
                    return;
                }

                if (_cloudAnchorSystem.AnchorModel.CloudMode == ApplicationMode.Hosting)
                {
                    _cloudAnchorSystem.HostLastPlacedAnchor();
                }
                else if (_cloudAnchorSystem.AnchorModel.CloudMode == ApplicationMode.Resolving)
                {
                    _cloudAnchorSystem.ResolveAnchorFromId(RoomManager.Instance.AnchorId);
                }

                _model.SetMode(UIMode.Activate);
            };

            _breederUIView.PetSystemUIView.PetActivationButton.onClickedCallback += () =>
            {
                if (!_cloudAnchorSystem.AnchorModel.IsTrackable.Value)
                {
                    return;
                }

                PetPresenter pet = PetPresenter.Create(_petRoot, _cloudAnchorSystem.AnchorModel.PlacedAnchorRoot.Value.transform.position);
                _petPresenter = pet;

                _model.SetMode(UIMode.Breed);
            };
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
