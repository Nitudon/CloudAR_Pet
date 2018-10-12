using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;
using UniRx;

public class GyroDetector : UdonBehaviour
{
    private Vector3 _centerAxis;
    public Vector3 CenterAxis => _centerAxis;

    private Vector3 _currentAngle;
    public Vector3 CurrentAngle => _currentAngle;

    public Vector3 _deltaAngle;
    public Vector3 DeltaAngle => _deltaAngle;

    private ReactiveProperty<GyroInfo> _inputGyroInfo;
    public IReadOnlyReactiveProperty<GyroInfo> InputGyroInfo => _inputGyroInfo;

    protected override void Start()
    {
        base.Start();
        Input.gyro.enabled = true;

        if (_inputGyroInfo == null)
        {
            _inputGyroInfo = new ReactiveProperty<GyroInfo>();
        }

        this
            .ObserveEveryValueChanged(_ => Input.gyro.attitude)
            .Subscribe(UpdateGyroInfo)
            .AddTo(gameObject);
    }

    public void Refresh()
    {
        _centerAxis = GetWorldAngleFromMobileQuaternion(Input.gyro.attitude);
    }

    private void UpdateGyroInfo(Quaternion quaternion)
    {
        _currentAngle = GetWorldAngleFromMobileQuaternion(quaternion);
        _deltaAngle = _currentAngle - _centerAxis;

        _inputGyroInfo.Value = new GyroInfo(_centerAxis, _currentAngle, _deltaAngle, Input.gyro.userAcceleration);
    }

    private Vector3 GetWorldAngleFromMobileQuaternion(Quaternion quaternion)
    {
        var worldQuaternion = new Quaternion(-quaternion.x, -quaternion.z, -quaternion.y, quaternion.w) * Quaternion.Euler(90f, 0f, 0f);

        return worldQuaternion.eulerAngles;
    }
}