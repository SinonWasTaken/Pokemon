using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Player_Controls controls;
    
    [SerializeField] private float walk_speed = 5;
    [SerializeField] private float run_speed = 8;

    [Header("Camera")] 
    [SerializeField] private GameObject camera;
    
    [SerializeField] private float rotation_speed = 5;
    [SerializeField] private float rotation_smoothing;

    private Vector3 ref_rotation;
    [SerializeField] private Vector2 camera_rotation;
    [SerializeField] private Vector3 rotation;
    
    [Header("Movement")]
    [SerializeField] private float move_rotation_speed;
    [SerializeField] private float move_rotate_smoothing;

    [SerializeField] private Vector2 player_movement;
    private float ref_movement;

    private byte sprint;

    private Rigidbody rb;
        
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        controls = new Player_Controls();
        controls.Controls.Camera_Rotation.performed += Camera_RotationOnPerformed;
        controls.Controls.Movement.performed += MovementOnPerformed;
        controls.Controls.Movement.canceled += MovementOnPerformed;
    }

    private void MovementOnPerformed(InputAction.CallbackContext obj)
    {
        player_movement = obj.ReadValue<Vector2>();
    }

    private void Camera_RotationOnPerformed(InputAction.CallbackContext obj)
    {
        Vector2 rot = obj.ReadValue<Vector2>();
        camera_rotation += new Vector2(rot.x * rotation_speed, -rot.y * rotation_speed);
    }

    private void Update()
    {
        if (Dialogue_System.Instance.DialogueActive == false)
        {
            if (BattleSystem.Instance.HasStarted == false)
            {
                update_camera();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Dialogue_System.Instance.DialogueActive == false)
        {
            if (BattleSystem.Instance.HasStarted == false)
            {
                update_movement();
            }
        }
    }

    private void update_camera()
    {
        rotation = Vector3.SmoothDamp(rotation, new Vector3(camera_rotation.y, camera_rotation.x), ref ref_rotation, rotation_smoothing);
        camera.transform.eulerAngles = rotation;

        camera.transform.position = transform.position - camera.transform.forward * 5;
    }

    private void update_movement()
    {
        Vector2 mag = player_movement.normalized;

        if (mag != Vector2.zero)
        {
            float rotation = Mathf.Atan2(mag.x, mag.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref ref_movement, move_rotate_smoothing);

            float speed = sprint == 1 ? run_speed : walk_speed;

            rb.MovePosition(rb.position + transform.forward * (speed * Time.fixedDeltaTime));
            //transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
