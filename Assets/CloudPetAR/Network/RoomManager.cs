using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdonLib.Commons;


namespace CloudPet.Network
{
    /// <summary>
    /// ルーム関連のマネージャー
    /// </summary>
    public class RoomManager : UdonBehaviourSingleton<RoomManager>
    {
        private RoomModel _model;
        public RoomModel Model => _model;

        protected override bool IsDontDestroy => true;

        public override void Initialize()
        {
            base.Initialize();

            _model = new RoomModel();
        }
    }
}
