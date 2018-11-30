using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieController : MonoBehaviour {

    public float lookRadius = 10f;

    Transform target;
    Transform safeHouse;

    NavMeshAgent agent;

    private float searchCountdown = 0.3f;

    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        safeHouse = GameObject.Find("Shed").transform;
        agent = GetComponent<NavMeshAgent>();      
    }
	
	// Update is called once per frame
	void Update () {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {

            searchCountdown = 0.3f;

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
            else
            {
                agent.SetDestination(safeHouse.position);
            }
        }
	}

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = (Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
