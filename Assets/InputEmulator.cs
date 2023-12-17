using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public sealed class InputEmulator : MonoBehaviour
{
    [field:SerializeField] bool AutoTouch { get; set; }
    [field:SerializeField] float Interval { get; set; } = 1;

    VisualEffect _vfx;

    void StartTouch()
    {
        _vfx.SendEvent("OnTouch");
        _vfx.SetBool("Touch", true);
    }

    void EndTouch()
      => _vfx.SetBool("Touch", false);

    IEnumerator Start()
    {
        _vfx = GetComponent<VisualEffect>();

        while (true)
        {
            if (AutoTouch) StartTouch();
            yield return new WaitForSeconds(Mathf.Max(0.01f, Interval));
            if (AutoTouch) EndTouch();
            yield return new WaitForSeconds(Mathf.Max(0.01f, Interval));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartTouch();
        if (Input.GetKeyUp(KeyCode.Space)) EndTouch();
    }
}
