using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControler : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent NMA;

    public Transform[] PatrolsPoints;
    public int currentIndex;

    public float AnticipationDistance;

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out NMA);
        NMA.destination = PatrolsPoints[0].position;
    }

    void Update()
    {
       
       if(NMA.remainingDistance <= AnticipationDistance)
        {
            //Debug.Log("Arriv� !");
            //changer la destination
            if((currentIndex + 1) < PatrolsPoints.Length)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            NMA.destination = PatrolsPoints[currentIndex].position;
        }
    }
}
