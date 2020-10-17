using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARVirtualButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private GameObject virtualButton;

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
            virtualButton.transform.SetPositionAndRotation(transform.position, transform.rotation);

            if (!virtualButton.activeSelf)
            {
                virtualButton.SetActive(true);
            }
        }

        var mainCamera = Camera.main;

        var cameraHit = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hitResult,
            Mathf.Infinity, 1 << 8);

        // Debug.DrawRay(camera.transform.position, camera.transform.forward * 10f, Color.cyan);

        // Transforma todos os VirtualButtons em vermelho
        // GameObject.FindGameObjectsWithTag("VirtualButton").ToList().ForEach(button =>
        // {
        //     button.GetComponent<MeshRenderer>().material.color = Color.red;
        // });

        GameObject selectedVirtualButton = null;

        // Se conseguiu atingir algum VirtualButton, muda a cor para verde
        if (cameraHit)
        {
            selectedVirtualButton = hitResult.transform.gameObject;

            // hitResult.transform.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        if (Input.GetMouseButtonDown(0) && selectedVirtualButton != null)
        {
            Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.OpenURL.html");
        }
    }
}
