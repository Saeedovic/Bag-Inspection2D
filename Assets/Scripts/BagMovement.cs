using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMovement : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(10, transform.position.y, transform.position.z); 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

        if (transform.position == endPos)
        {
            transform.position = startPos;
        }
    }

    public void MoveToEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
    