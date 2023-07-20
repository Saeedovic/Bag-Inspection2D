using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 1f;
    public float waitTime = 2f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            
            float step = speed * Time.deltaTime;
            while (Vector2.Distance(transform.position, pointB) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointB, step);
                yield return null;
            }

            
            yield return new WaitForSeconds(waitTime);

            
            while (Vector2.Distance(transform.position, pointA) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointA, step);
                yield return null;
            }

            
            yield return new WaitForSeconds(waitTime);
        }
    }
}

