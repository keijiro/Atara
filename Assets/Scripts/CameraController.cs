using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public sealed class CameraController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField] public float3 BaseAngles { get; set; } = 45;
    [field:SerializeField] public float PanWidth { get; set; } = 180;
    [field:SerializeField] public uint RandomSeed { get; set; } = 1234;

    #endregion

    #region Private members

    Random _random;

    float MousePosition
      => Input.mousePosition.x / Screen.width - 0.5f;

    quaternion NextRandomRotation
      => quaternion.EulerZXY
           (_random.NextFloat3(-1, 1) * math.radians(BaseAngles));

    #endregion

    #region MonoBehaviour implementation

    void Start()
      => _random = new Random(RandomSeed);

    void Update()
    {
        var xformOffs = transform.parent;
        var xformPan = transform.parent.parent;

        if (Input.GetMouseButtonDown(0))
            xformOffs.localRotation = NextRandomRotation;

        if (Input.GetMouseButton(0))
            xformPan.localRotation =
              quaternion.RotateY(MousePosition * math.radians(PanWidth));
    }

    #endregion
}
