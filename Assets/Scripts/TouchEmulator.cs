using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

public sealed class TouchEmulator : MonoBehaviour
{
    #region Public properties

    [field:SerializeField] float OnDuration { get; set; } = 1;
    [field:SerializeField] float OffDuration { get; set; } = 1;

    #endregion

    #region Private members

    float RandomFactor => Random.Range(0.5f, 1);

    #endregion

    #region MonoBehaviour implementation

    IEnumerator Start()
    {
        var handle = GetComponent<TouchHandler>();

        while (true)
        {
            handle.ForceTouch = true;
            yield return new WaitForSeconds(RandomFactor * OnDuration);
            handle.ForceTouch = false;
            yield return new WaitForSeconds(RandomFactor * OffDuration);
        }
    }

    #endregion
}
