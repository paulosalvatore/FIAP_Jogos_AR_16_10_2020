using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private GameObject spider;

    [SerializeField]
    private GameObject targetMarker;

    private void Update()
    {
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        var screenCenter = new Vector2(x, y);

        var hitResults = new List<ARRaycastHit>();

        arRaycastManager.Raycast(screenCenter, hitResults, TrackableType.Planes);

        if (hitResults.Count > 0)
        {
            var hit = hitResults[0];

            transform.SetPositionAndRotation(hit.pose.position, hit.pose.rotation);

            if (!targetMarker.activeSelf)
            {
                targetMarker.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0) && targetMarker.activeSelf)
        {
            var spiderClone = Instantiate(spider, transform.position, transform.rotation);
            spiderClone.SetActive(true);
        }
    }
}
