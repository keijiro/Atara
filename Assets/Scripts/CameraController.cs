using Klak.Math;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public sealed class CameraController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField] public float3 BaseAngles { get; set; } = 45;
    [field:SerializeField] public float PanWidth { get; set; } = 180;
    [field:SerializeField] public float TweenSpeed { get; set; } = 10;
    [field:SerializeField] public uint RandomSeed { get; set; } = 1234;

    #endregion

    #region Private members

    Random _random;
    quaternion _rotation;

    float MousePosition
      => Input.mousePosition.x / Screen.width - 0.5f;

    quaternion NextRandomRotation
      => quaternion.EulerZXY(_random.NextFloat3(-1, 1) * BaseAngles);

    #endregion

    #region MonoBehaviour implementation

    void Start()
      => (_random, _rotation) = (new Random(RandomSeed), quaternion.identity);

    void Update()
    {
        var xform1 = transform.parent.parent;
        var xform2 = transform.parent;

        if (Input.GetMouseButtonDown(0)) _rotation = NextRandomRotation;

        if (Input.GetMouseButton(0))
            xform1.localRotation =
              Quaternion.AngleAxis(MousePosition * PanWidth, Vector3.up);

        xform2.localRotation =
            ExpTween.Step(xform2.localRotation, _rotation, TweenSpeed);
    }

    #endregion
}
