using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiFolow : MonoBehaviour
{
    private NavMeshAgent agent;      
    private Animator anim;           
    private State currentState;      

    public Transform player;         

    public LayerMask groundLayer;    
    public float groundCheck = 1f;   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
        anim = GetComponent<Animator>();         
        currentState = new Idle(gameObject, agent, anim, player); 
    }

    void Update()
    {
        currentState = currentState.Process(); 
        AdjustHeight(); 
    }

    private void AdjustHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheck, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}

