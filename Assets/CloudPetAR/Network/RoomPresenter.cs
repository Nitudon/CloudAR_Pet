﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

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
                .Subscribe()
                .AddTo(gameObject);

            _roomConnector.Model
                .onRoomJoined
                .Subscribe()
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
            SceneManager.Instance.SceneLoad();
        }
    }
}
