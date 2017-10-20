using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRStandardAssets.Utils;
public class CameraRayCaster : MonoBehaviour
{
    public event Action<RaycastHit> OnRaycasthit;                   // This event is called every frame that the user's gaze is over a collider.

    [SerializeField] private Transform m_Camera;
    [SerializeField] private LayerMask m_ExclusionLayers;
    [SerializeField] private Reticle m_Reticle;
    [SerializeField] private float m_RayLength = 500f;
    [SerializeField] private InteractableObject currentInteractible;

    [SerializeField] private InteractableObject lastObjectInSights;
    // Use this for initialization

    [SerializeField] private bool LockedOn = false;
    private void Update()
    {
        if (!LockedOn)
        {
            EyeRaycast();
        }
        else
        {
            EyeRaycastTarget();
        }
    }
    private void EyeRaycastTarget()
    {
        RaycastHit hit;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(lastObjectInSights.transform.position);
        if (!(screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
        {
            LockedOn = false;
            return;
        }
        if (Physics.Raycast(transform.position, (lastObjectInSights.transform.position - transform.position), out hit, m_RayLength))
        {
            m_Reticle.SetPosition(hit);
        }
        else
        {
            LockedOn = false;
        }
    }
    private void EyeRaycast()
    {

        // Create a ray that points forwards from the camera.
        Ray ray = new Ray(m_Camera.position, m_Camera.forward);
        RaycastHit hit;
        // Do the raycast forweards to see if we hit an interactive item
        if (Physics.Raycast(ray, out hit, m_RayLength, ~m_ExclusionLayers))
        {
            InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
            currentInteractible = obj;
            // Deactive the last interactive item 
            if (obj != lastObjectInSights)
            {
                DeactiveLastInteractible();
            }
            if (obj && obj != lastObjectInSights)
            {
                obj.Over();
            }
            lastObjectInSights = obj;

            // Something was hit, set at the hit position.
            if (m_Reticle)
                m_Reticle.SetPosition(hit);

            if (OnRaycasthit != null)
                OnRaycasthit(hit);
        }
        else
        {
            // Nothing was hit, deactive the last interactive item.
            DeactiveLastInteractible();
            currentInteractible = null;

            // Position the reticle at default distance.
            if (m_Reticle)
                m_Reticle.SetPosition();
        }
    }
    private void DeactiveLastInteractible()
    {
        if (lastObjectInSights == null)
            return;

        lastObjectInSights.Out();
        lastObjectInSights = null;
    }

    public void LockOnEnemy()
    {
        if (currentInteractible && !LockedOn)
        {
            Enemy enemy = currentInteractible.GetComponent<Enemy>();
            if (enemy)
            {
                LockedOn = true;
            }
        }
        else
        {
            LockedOn = false;
            m_Reticle.ResetPosition();
        }
    }
}
