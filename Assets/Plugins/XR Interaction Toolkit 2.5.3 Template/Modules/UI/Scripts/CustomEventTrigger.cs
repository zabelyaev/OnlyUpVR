using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class CustomEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent hoverEntered;
    public UnityEvent hoverExited;
    public UnityEvent click;

    public void OnPointerClick(PointerEventData eventData)
    {
        click.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData is TrackedDeviceEventData trackedDeviceEventData)
        {
            if (trackedDeviceEventData.interactor is XRBaseControllerInteractor xrInteractor)
            {
                xrInteractor.SendHapticImpulse(0.25f, 0.25f);
            }
        }

        hoverEntered.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverExited.Invoke();
    }
}
