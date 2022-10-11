using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    #region Singltone
    public static PlayerCamera Instance
    {
        get
        {
            return _instance;
        }
    }
    private static PlayerCamera _instance;
    #endregion

    [SerializeField] public GameObject CameraObj;
    [SerializeField] private Camera _myCamera;
    [SerializeField] private Camera _mainCamera;
    private float _mouseX;
    private float _mouseYRoration;

    #region Get/Set

    public bool TryGetMyCamera(out Camera camera)
    {
        if (_myCamera)
        {
            camera = _myCamera;
            return true;
        }
        else
        {
            camera = null;
            return false;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _myCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {      
        SwitchCamera();
    }

    public void SwitchCamera()
    {
        _mainCamera.enabled = !_mainCamera.enabled;
        _myCamera.enabled = !_myCamera.enabled;
    }

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse Y") * InputListener.Instance.SENSIVITY * Time.deltaTime;
        _mouseYRoration -= _mouseX;
        _mouseYRoration = Mathf.Clamp(_mouseYRoration, -90f, 90f);

        _myCamera.transform.localRotation = Quaternion.Euler(_mouseYRoration, 0f, 0f);
    }
}