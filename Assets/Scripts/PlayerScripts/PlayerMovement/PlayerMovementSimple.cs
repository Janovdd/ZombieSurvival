using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour {
    private Quaternion targetRotation;

    public float rotationSpeed;
    public float walkSpeed;
    Animator anim;
    private float currentStrength;
    private float maxStrength;
    public float recoveryRate;
    public GameObject parent;
    public Joystick joyStick;

    void Start() {
        parent.transform.eulerAngles = new Vector3(0, 45, 0);
        anim = GetComponent<Animator>();
    }

    
    void Update()
    { 
            JoyStickMove();    
    }

    public void OneHandControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            TwoHandController();
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            TwoHandController();
            transform.eulerAngles = new Vector3(0, -45, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            TwoHandController();
            transform.eulerAngles = new Vector3(0, 225, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            TwoHandController();
            transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else
        {
            currentStrength = Mathf.MoveTowards(currentStrength, maxStrength, recoveryRate * Time.deltaTime);

            if (Input.GetKey(KeyCode.W))
            {
                maxStrength = 1f;
                anim.SetFloat("Forward", currentStrength);
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                maxStrength = 1f;
                anim.SetFloat("Forward", currentStrength);
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                maxStrength = 1f;
                anim.SetFloat("Forward", currentStrength);
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                maxStrength = 1f;
                anim.SetFloat("Forward", currentStrength);
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else
            {
                maxStrength = 0f;
                anim.SetFloat("Forward", currentStrength);
            }

            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (input != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(input);
                transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y + 45, rotationSpeed * Time.deltaTime);
            }
        }
    
    }

    public void TwoHandController()
    {
        currentStrength = Mathf.MoveTowards(currentStrength, maxStrength, recoveryRate * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            maxStrength = 1f;
            anim.SetFloat("Forward", currentStrength);
            parent.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            maxStrength = -1f;
            anim.SetFloat("Turn", currentStrength);
            parent.transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            maxStrength = -1f;
            anim.SetFloat("Forward", currentStrength);
            parent.transform.Translate(Vector3.back * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            maxStrength = 1f;
            anim.SetFloat("Turn", currentStrength);
            parent.transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
        }
        else
        {
            maxStrength = 0f;
            anim.SetFloat("Forward", currentStrength);
            anim.SetFloat("Turn", currentStrength);
        }
    }

    public void JoyStickMove()
    {
        currentStrength = Mathf.MoveTowards(currentStrength, maxStrength, recoveryRate * Time.deltaTime);

        float x = joyStick.Horizontal * walkSpeed * Time.deltaTime;
        float z = joyStick.Vertical * walkSpeed * Time.deltaTime;

        parent.transform.Translate(x, 0, z);

        Vector3 input = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y + 45, rotationSpeed * Time.deltaTime);
        }

        if (x != 0 || z != 0)
        {
            maxStrength = 1f;
            anim.SetFloat("Forward", currentStrength);
        }
        else
        {
            maxStrength = 0f;
            anim.SetFloat("Forward", currentStrength);
        }
    }
        
}
