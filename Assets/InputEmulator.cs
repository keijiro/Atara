using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public sealed class InputEmulator : MonoBehaviour
{
    [field:SerializeField] bool AutoTouch { get; set; }
    [field:SerializeField] float Interval { get; set; } = 1;

    VisualEffect[] _vfxCache;

    static int ID_Touch = Shader.PropertyToID("Touch");
    static int ID_OnTouch = Shader.PropertyToID("OnTouch");

    void StartTouch()
    {
        foreach (var vfx in _vfxCache)
        {
            vfx.SendEvent(ID_OnTouch);
            if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, true);
        }
    }

    void EndTouch()
    {
        foreach (var vfx in _vfxCache)
            if (vfx.HasBool(ID_Touch)) vfx.SetBool(ID_Touch, false);
    }

    IEnumerator Start()
    {
        _vfxCache = GetComponentsInChildren<VisualEffect>();

        for (var wait = new WaitForSeconds(Mathf.Max(0.01f, Interval));;)
        {
            if (AutoTouch) StartTouch();
            yield return wait;
            if (AutoTouch) EndTouch();
            yield return wait;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartTouch();
        if (Input.GetKeyUp(KeyCode.Space)) EndTouch();
    }
}
