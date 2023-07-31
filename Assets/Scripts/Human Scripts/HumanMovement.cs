using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    public float speed = 1f;
    public Transform startPosObj;
    public Transform stopPosObj;
    public Transform endPosObj;
    private Vector3 startPos;
    private Vector3 stopPos;
    private Vector3 endPos;

    public HumanItemPlacement humanItemPlacement;

    private void Start()
    {
        startPos = startPosObj.position;
        stopPos = stopPosObj.position;
        endPos = endPosObj.position;
        StartCoroutine(MoveToStop());
    }

    public IEnumerator MoveToStart()
    {
        while (Vector3.Distance(transform.position, startPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            yield return null;
        }

        ResetToStart();
        humanItemPlacement.ClearItems();
        StartCoroutine(MoveToStop());
    }

    public IEnumerator MoveToStop()
    {
        while (Vector3.Distance(transform.position, stopPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPos, speed * Time.deltaTime);
            yield return null;
        }

        humanItemPlacement.PlaceItems();
    }

    public IEnumerator MoveToLast()
    {
        while (Vector3.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        ResetToStart();
        humanItemPlacement.ClearItems();
        StartCoroutine(MoveToStop());
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}