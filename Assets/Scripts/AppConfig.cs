using UnityEngine;

public sealed class AppConfig : MonoBehaviour
{
    [field:SerializeField] public int TargetFrameRate { get; set; } = 60;

    void Start()
      => Application.targetFrameRate = TargetFrameRate;
}
