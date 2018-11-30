using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunToHand : MonoBehaviour {
    private Quaternion targetRotation;

    public float rotationSpeed;

    public GameObject player;

    void Update()
    {
        Vector3 pos = player.transform.position;

        transform.position = pos;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y - 45, rotationSpeed * Time.deltaTime);
        }
    }


}
