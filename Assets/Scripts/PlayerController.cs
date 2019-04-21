using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GravityBody))]
public class PlayerController : MonoBehaviour
{

    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float jumpForce = 220;
    public LayerMask groundedMask;
    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    public Transform cameraTransform;
    Rigidbody rigidbody;
    private float _timeFromPrevSound;

    public AudioSource Source;
    public SeasonsManager Manager;

    [SerializeField] private float _delayBetweenSounds = 0.5f;
    
    void Awake() {
        rigidbody = GetComponent<Rigidbody> ();
    }
	
    void Update() {
		
        // Look rotation:
       // if (Input.GetMouseButton(0))
        //{
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
            //verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
            //verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
            //cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
       // }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
             Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            && (Time.time - _timeFromPrevSound) > _delayBetweenSounds)
        {
            Source.PlayOneShot( Manager.SeasonCycle[Manager.CurrentSeason].Footsteps[Random.Range(0,2)] );
            _timeFromPrevSound = Time.time;
        }
        
        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal") * -1;
        float inputY = Input.GetAxisRaw("Vertical") * -1;
		
        Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
		
        // Jump
        if (Input.GetButtonDown("Jump")) {
            if (grounded) {
                rigidbody.AddForce(transform.up * jumpForce);
            }
        }
		
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
		
        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
            grounded = true;
        }
        else {
            grounded = false;
        }
		
    }
	
    void FixedUpdate() {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}
