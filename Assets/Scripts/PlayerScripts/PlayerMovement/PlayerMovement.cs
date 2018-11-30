using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    Animator anim;
    public float speed;
    Vector3 lookPos;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;
    

	// Use this for initialization
	void Start () {
        SetupAnimator();

        cam = Camera.main.transform;
	}
	
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * camForward + horizontal * cam.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(move);

        transform.Translate(horizontal, 0, vertical);

     
    }

    void Move(Vector3 move)
    {
        if(move.magnitude > 1){
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x * 25f;

        forwardAmount = localMove.z * 25f;
    }

    void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.5f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.5f, Time.deltaTime);
    }

    void SetupAnimator()
    {
        anim = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}
