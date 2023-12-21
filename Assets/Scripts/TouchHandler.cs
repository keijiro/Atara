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

    Vector2 InvalidTouchPosition
      => new Vector2(-1, -1);

    Vector2 NormalizedTouchPosition
      => new Vector2(Mathf.Clamp01(Input.mousePosition.x / Screen.width),
                     Mathf.Clamp01(Input.mousePosition.y / Screen.height));

    #endregion

    #region MonoBehaviour implementation

    IEnumerator Start()
    {
        var idTouch = Shader.PropertyToID("Touch");
        var vfxList = GetComponentsInChildren<VisualEffect>();

        while (true)
        {
            foreach (var vfx in vfxList)
                if (vfx.HasVector2(idTouch))
                    vfx.SetVector2(idTouch, InvalidTouchPosition);

            while (!IsTouchOn) yield return null;

            foreach (var vfx in vfxList) vfx.Play();

            while (IsTouchOn)
            {
                foreach (var vfx in vfxList)
                    if (vfx.HasVector2(idTouch))
                        vfx.SetVector2(idTouch, NormalizedTouchPosition);
                yield return null;
            }
        }
    }

    #endregion
}
