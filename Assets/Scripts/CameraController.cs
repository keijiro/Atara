using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField] public float PanWidth { get; set; } = 90;

    #endregion

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            transform.parent.localRotation =
                Quaternion.Euler(Random.Range(-45, 45),
                                 Random.Range(-45, 45),
                                 Random.Range(-90, 90));

        if (Input.GetMouseButton(0))
        {
            var x = (float)Input.mousePosition.x / Screen.width;
            transform.parent.parent.localRotation =
              Quaternion.AngleAxis((x - 0.5f) * PanWidth, Vector3.up);
        }
    }
}
