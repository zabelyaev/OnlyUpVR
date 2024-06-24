using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIReticleManager : MonoBehaviour
{
    [SerializeField] private InputActionProperty trigerAction;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject reticleActivate;

    private XRInteractorLineVisual lineVisual;

    private void Awake()
    {
        lineVisual = GetComponent<XRInteractorLineVisual>();
    }

    void Start()
    {
        trigerAction.action.performed += TriggerPressed;
        trigerAction.action.canceled += TriggerCanceled;
    }

    private void TriggerCanceled(InputAction.CallbackContext obj)
    {
        lineVisual.reticle = reticle;
    }

    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        lineVisual.reticle = reticleActivate;
    }
}