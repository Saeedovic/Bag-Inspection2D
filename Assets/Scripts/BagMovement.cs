using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMovement : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 startPos;
    private Vector3 endPos;
    public ItemPlacement itemPlacement; 

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3((float)-0.6, transform.position.y, transform.position.z);
        StartCoroutine(MoveToEnd()); 
    }

    public IEnumerator MoveToEnd()
    {
        while (Vector3.Distance(transform.position, endPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            yield return null;
        }
        
        itemPlacement.PlaceItems();
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
