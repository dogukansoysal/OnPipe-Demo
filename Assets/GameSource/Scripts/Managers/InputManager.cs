using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class InputManager : Singleton<InputManager>
{
    public UnityAction<Vector3> swerve;
    public UnityAction<Vector3> swipe;
    public UnityAction<Vector3> raycast;
    public UnityAction tap;
    public UnityAction<float> hold;
    private float heldTimeCounter;
    private Vector3 startPos;
    public LayerMask raycastLayer;
    
    private void Update()
    {
        if(GameManager.Instance.gameState != GameState.Play)
        {
            return;
        }
        Vector3 deltaPos = new Vector3();
        if (Input.GetMouseButtonDown(0))
        {
            if (tap != null)
            {
                tap.Invoke();
            }
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            //Swerve
            //deltaPos = (Input.mousePosition- startPos) /Screen.dpi;
            deltaPos = Input.mousePosition / Screen.dpi;
            if(swerve != null)
            {
                swerve.Invoke(deltaPos);
            }
            //-----
            //Raycast
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100, raycastLayer))
            {
                if(raycast != null)
                {
                    raycast.Invoke(hit.point);
                }
            }
            //------
            //Hold
            heldTimeCounter += Time.deltaTime;
            if(hold != null)
            {
                hold.Invoke(heldTimeCounter);
            }
            //----
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Swipe
            heldTimeCounter = 0;
            deltaPos = (Input.mousePosition- startPos).normalized;
            if(swipe != null)
            {
                swipe.Invoke(deltaPos);
            }
            //----
        }
    }
    
}