using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetHandPokePoint : MonoBehaviour
{
    [SerializeField] private Transform pokeHandPoint;

    private void Start()
    {
        this.gameObject.transform.parent.parent.GetComponentInChildren<XRPokeInteractor>().attachTransform = pokeHandPoint;           
    }
}
