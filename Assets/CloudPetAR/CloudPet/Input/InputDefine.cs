using UnityEngine;

public struct GyroInfo
{
    public readonly Vector3 CenterAxis;
    public readonly Vector3 CurrentAngle;
    public readonly Vector3 DeltaAngle;
    public readonly Vector3 Acceralation;

    public GyroInfo(Vector3 center, Vector3 current, Vector3 delta, Vector3 accelaration)
    {
        CenterAxis = center;
        CurrentAngle = current;
        DeltaAngle = delta;
        Acceralation = accelaration;
    }
}