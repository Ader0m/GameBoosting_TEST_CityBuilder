using UnityEngine;
using UnityEngine.EventSystems;

public class InputListener : MonoBehaviour
{
    #region Singltone
    public static InputListener Instance
    {
        get
        {
            return _instance;
        }
    }
    private static InputListener _instance;
    #endregion

    [SerializeField] private GameObject _menu;
    [SerializeField] public float SENSIVITY = 850;
    [SerializeField] public float MOVEMENT_SPEED = 500;
    private Player _player;
    private Vector3 _movementVector; 
    private uint _raycastButton;

    #region Get/Set

    public Vector3 MovementVector
    {
        get
        {
            return _movementVector;
        }
    }
    public uint RaycastBytton
    {
        get
        {
            return _raycastButton;
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    #endregion

    void Start()
    {
        _raycastButton = 0;
        _movementVector = new Vector3();
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            _raycastButton++;
        }
    }

    void FixedUpdate()
    {       
        if (_player && !_menu.activeSelf)
            MoveInput();
    }

    private void MoveInput()
    {
        _movementVector.x = Input.GetAxis("Horizontal");
        //_movementVector.y = Input.GetKey("space") ? 1f : 0f;
        //_movementVector.y = Input.GetKey(KeyCode.LeftControl) ? -1f : 0f;
        _movementVector.z = Input.GetAxis("Vertical");
        
    }
}