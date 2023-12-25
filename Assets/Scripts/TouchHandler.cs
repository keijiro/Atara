using UnityEngine;
using UnityEngine.VFX;
using IEnumerator = System.Collections.IEnumerator;

public sealed class TouchHandler : MonoBehaviour
{
    #region Private members

    bool IsTouchOn => Input.GetMouseButton(0);

    Vector3 InvalidTouchPosition
      => new Vector3(0.5f, 0.5f, 0);

    Vector3 NormalizedTouchPosition
      => new Vector3(Mathf.Clamp01(Input.mousePosition.x / Screen.width),
                     Mathf.Clamp01(Input.mousePosition.y / Screen.height), 1);

    #endregion

    #region MonoBehaviour implementation

    IEnumerator Start()
    {
        var idTouch = Shader.PropertyToID("Touch");
        var vfxList = GetComponentsInChildren<VisualEffect>();

        while (true)
        {
            foreach (var vfx in vfxList)
                if (vfx.HasVector3(idTouch))
                    vfx.SetVector3(idTouch, InvalidTouchPosition);

            while (!IsTouchOn) yield return null;

            foreach (var vfx in vfxList) vfx.Play();

            while (IsTouchOn)
            {
                foreach (var vfx in vfxList)
                    if (vfx.HasVector3(idTouch))
                        vfx.SetVector3(idTouch, NormalizedTouchPosition);
                yield return null;
            }

            foreach (var vfx in vfxList) vfx.Stop();
        }
    }

    #endregion
}
