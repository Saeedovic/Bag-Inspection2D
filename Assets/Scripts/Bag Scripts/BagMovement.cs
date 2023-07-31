using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMovement : MonoBehaviour
{
    public float speed = 1f;
    public Transform startPosObj;
    public Transform stopPosObj; 
    public Transform endPosObj; 
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 endPos;

    public ItemPlacement itemPlacement;

    private void Start()
    {
        startPos = startPosObj.position;
        stopPos = stopPosObj.position; 
        endPos = endPosObj.position; 
        StartCoroutine(MoveToEnd());
    }

    public IEnumerator MoveToEnd()
    {
        while (Vector3.Distance(transform.position, stopPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPos, speed * Time.deltaTime);
            yield return null;
        }

        itemPlacement.PlaceItems();
    }

    public IEnumerator MoveToLast()
    {
        while (Vector3.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        ResetToStart();
        itemPlacement.ClearItems();
        StartCoroutine(MoveToEnd());
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
