﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx;
using CloudPet.Common;

namespace CloudPet.Network
{
    public class RoomPresenter : InitializableMono
    {
        [SerializeField]
        private RoomConnector _roomConnector;

        [SerializeField]
        private RoomUIView _roomUIView;

        public override void Initialize()
        {
            SetEvent();
            Bind();
        }

        private void SetEvent()
        {
            _roomUIView.JoinRoomButton.onClick.AddListener(async () => await _roomConnector.ConnectRoom());
            _roomUIView.CreateRoomButton.onClick.AddListener(async () => await _roomConnector.CreateRoom());
        }

        private void Bind()
        {
            _roomConnector.Model
                .onRoomCreated
                .Subscribe(_ => LoadRoomScene())
                .AddTo(gameObject);

            _roomConnector.Model
                .onRoomJoined
                .Subscribe(_ => LoadRoomScene())
                .AddTo(gameObject);
        }

        private async Task OpenCreateRoomDialog()
        {
            await _roomConnector.ConnectRoom();
        }

        private async Task OpenJoinRoomDialog()
        {
            await _roomConnector.CreateRoom();
        }

        private void LoadRoomScene()
        {
            SceneManager.Instance.SceneLoad(CommonUtility.GetSceneName(SceneEnum.Room));
        }
    }
}
