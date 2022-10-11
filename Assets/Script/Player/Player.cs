using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxVelosity;
    private Rigidbody _rb;
    private float _mouseY;

    #region Get/Set    

    public Rigidbody rb
    {
        get
        {
            return _rb;
        }
    }

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();        
        InputListener.Instance.SetPlayer(this);                
    }

    void FixedUpdate()
    {      
        MoveLogick();
        RotateLogick();
        SpeedController();                
    }

    private void RotateLogick()
    {
        _mouseY = Input.GetAxis("Mouse X") * InputListener.Instance.SENSIVITY * Time.deltaTime;

        transform.Rotate(Vector3.up * _mouseY);
    }

    private void MoveLogick()
    {
        _rb.AddForce(
                    transform.forward *
                    InputListener.Instance.MovementVector.z *
                    InputListener.Instance.MOVEMENT_SPEED *
                    Time.deltaTime
                );
        _rb.AddForce(
                    transform.right *
                    InputListener.Instance.MovementVector.x *
                    InputListener.Instance.MOVEMENT_SPEED *
                    Time.deltaTime
                    );
        //_rb.AddForce(
        //            transform.up *
        //            InputListener.Instance.MovementVector.y *
        //            InputListener.Instance.MOVEMENT_SPEED *
        //            Time.deltaTime
        //            );
    }

    private void SpeedController()
    {             
        if (_rb.velocity.magnitude > _maxVelosity)
        {
            _rb.velocity = _rb.velocity.normalized * _maxVelosity;
        }
        //drag
        if (InputListener.Instance.MovementVector.z == 0)
        {           
             Vector3 newVelosity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z * Mathf.Clamp01(1f - 1f * Time.deltaTime));
            _rb.velocity = newVelosity;         
        }
        if (InputListener.Instance.MovementVector.x == 0)
        {          
            Vector3 newVelosity = new Vector3(_rb.velocity.x * Mathf.Clamp01(1f - 1f * Time.deltaTime), _rb.velocity.y, _rb.velocity.z);
            _rb.velocity = newVelosity;        
        }
        //if (InputListener.Instance.MovementVector.y == 0)
        //{                               
        //    Vector3 newVelosity = new Vector3 (_rb.velocity.x, _rb.velocity.y * Mathf.Clamp01(1f - 1f * Time.deltaTime), _rb.velocity.z);               
        //    _rb.velocity = newVelosity;
        //}        
    }
}