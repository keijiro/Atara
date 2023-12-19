using UnityEngine;
using UnityEngine.VFX;
using IEnumerator = System.Collections.IEnumerator;

public sealed class TouchHandler : MonoBehaviour
{
    #region Public properties

    public bool ForceTouch { get; set; }

    #endregion

    #region Private members

    bool IsTouchOn => Input.GetMouseButton(0) || ForceTouch;

    Vector2 NormalizedMousePosition
      => new Vector2(Input.mousePosition.x / Screen.width,
                     Input.mousePosition.y / Screen.height);

    VisualEffect[] _vfxList;

    #endregion

    #region Predefined constants

    static readonly int ID_Touch = Shader.PropertyToID("Touch");
    static readonly int ID_TouchPosition = Shader.PropertyToID("TouchPosition");

    #endregion

    #region MonoBehaviour implementation

    IEnumerator Start()
    {
        _vfxList = GetComponentsInChildren<VisualEffect>();

        while (true)
        {
            while (!IsTouchOn) yield return null;

            foreach (var vfx in _vfxList)
            {
                if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, true);
                vfx.Play();
            }

            yield return null;

            while (IsTouchOn) yield return null;

            foreach (var vfx in _vfxList)
            {
                if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, false);
                vfx.Stop();
            }

            yield return null;
        }
    }

    void Update()
    {
        foreach (var vfx in _vfxList)
            if (vfx.HasVector2(ID_TouchPosition))
                vfx.SetVector2(ID_TouchPosition, NormalizedMousePosition);
    }

    #endregion
}
