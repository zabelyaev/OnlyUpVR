using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[DisallowMultipleComponent]
public class CustomXRGrabInteractable : XRGrabInteractable
{
    #region Private Fields

    private Collider[] _childColliders;
    private XROrigin _player;

    private Vector3 _initPos;
    private Quaternion _initRot;
    private Rigidbody _rigidbody;

    private bool _objectIsGrabbed = false;

    #endregion

    #region Serialize Fields

    [Header("Custom Parameters")]

    [SerializeField] private bool _ignoreCollision = true;

    public bool IgnoreCollision
    {
        get => _ignoreCollision;
        set
        {
            SetIgnoreCollision(value); _ignoreCollision = value;
        }
    }

    [SerializeField] private bool _needReturnObjectToInitPos = false;

    public bool NeedReturnObjectToInitPos { get => _needReturnObjectToInitPos; set => _needReturnObjectToInitPos = value; }

    [SerializeField] private float _timeToReturnInitPos = 0f;

    public float TimeToReturnInitPos { get => _timeToReturnInitPos; set => _timeToReturnInitPos = value; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();

        _player = GameObject.FindObjectOfType<XROrigin>();
        _childColliders = this.gameObject.GetComponentsInChildren<Collider>();

        if (_needReturnObjectToInitPos)
        {
            _initPos = this.transform.position;
            _initRot = this.transform.rotation;
        }
    }

    #region Overrided XRGrabInteractable Methods

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        _objectIsGrabbed = true;

        // если быстро подобрали предмет или он попал в сокет, то он не должен возвращаться на исходную позицию
        CancelInvoke(nameof(ReturnObjectToInitPos));

        if (_ignoreCollision)
        {
            SetIgnoreCollision(true);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        _objectIsGrabbed = false;

        if (_ignoreCollision)
        {
            SetIgnoreCollision(false);
        }

        ReturnObject();
    }

    #endregion

    #region Private Methods

    private void SetIgnoreCollision(bool ignore)
    {
        foreach (var colldier in _childColliders)
        {
            Physics.IgnoreCollision(colldier, _player.gameObject.GetComponent<Collider>(), ignore);
        }
    }

    private void ReturnObjectToInitPos()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = _initPos;
        transform.rotation = _initRot;
    }

    #endregion

    #region Public Methods

    public void ReturnObject()
    {
        if (_needReturnObjectToInitPos && !_objectIsGrabbed)
        {
            Invoke(nameof(ReturnObjectToInitPos), _timeToReturnInitPos);
        }
    }

    #endregion
}