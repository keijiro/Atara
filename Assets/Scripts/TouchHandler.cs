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

    #endregion

    #region Predefined constants

    static readonly int ID_Touch = Shader.PropertyToID("Touch");

    #endregion

    #region MonoBehaviour implementation

    IEnumerator Start()
    {
        var vfxList = GetComponentsInChildren<VisualEffect>();

        while (true)
        {
            while (!IsTouchOn) yield return null;

            foreach (var vfx in vfxList)
            {
                if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, true);
                vfx.Play();
            }

            yield return null;

            while (IsTouchOn) yield return null;

            foreach (var vfx in vfxList)
            {
                if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, false);
                vfx.Stop();
            }

            yield return null;
        }
    }

    #endregion
}
