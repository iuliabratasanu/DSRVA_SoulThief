using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private float waitTime;
    private float timer;
    private bool isMoving; // check if NPC should move

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false; // plane
        waitTime = 5f;
        timer = waitTime;
        isMoving = true; 
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (timer <= 0)
            {
                if (isMoving)
                {
                    Vector3 newDestination = GetRandomDestination();
                    if (newDestination != Vector3.zero)
                    {
                        agent.SetDestination(newDestination);
                    }
                    else
                    {
                        Debug.LogWarning("No valid destination found!");
                    }
                }

                timer = waitTime; // reset
            }
            else
            {
                timer -= Time.deltaTime; // - time passed since the last frame
            }

            yield return null;
        }
    }

    Vector3 GetRandomDestination()
    {
        float radius = 10f;
        Vector3 randomDirection = Random.insideUnitCircle * radius;
        randomDirection += transform.position;

        NavMeshHit hit;

        // check if pos is valid
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            Vector3 targetPosition = hit.position;
            targetPosition.z = transform.position.z;
            return targetPosition; 
        }
        return Vector3.zero; // position not valid
    }
}
