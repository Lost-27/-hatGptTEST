using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 90.0f;
    private CharacterController cc;
    
    [Header("GroundCheck Settings")] 
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundDistance = 0.1f;
    [SerializeField] private LayerMask _groundMask;
    
    [Header("Gravity Settings")] 
    [SerializeField] private float _gravityMultiplier = 1f;

    private Vector3 _fallVelocity;
    private bool _isGrounded;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundDistance, _groundMask);

        if (_isGrounded && _fallVelocity.y < 0)
        {
            _fallVelocity.y = -2f;
        }

        _fallVelocity += Physics.gravity * (_gravityMultiplier * Time.deltaTime);

        cc.Move(_fallVelocity * Time.deltaTime);
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, vertical);
        movement = transform.TransformDirection(movement);
        movement *= speed * Time.deltaTime;

        cc.Move(movement);

        transform.Rotate(0.0f, horizontal * rotationSpeed * Time.deltaTime, 0.0f);
    }
    
    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.CompareTag("Enemy"))
    //     {
    //         Vector3 pushDirection = transform.position - hit.gameObject.transform.position;
    //         pushDirection = pushDirection.normalized;
    //
    //         cc.Move(pushDirection * 10 * Time.deltaTime);
    //     }
    // }
}