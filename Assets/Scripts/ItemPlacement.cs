using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public List<GameObject> itemPrefabs; 
    public GameObject bag; 
    public Transform[] spawnPositions; 

    private void Start()
    {
        PlaceItems();
    }

    public void PlaceItems()
    {
        ClearItems();

        foreach (Transform spawnPosition in spawnPositions)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject selectedItemPrefab = itemPrefabs[randomIndex];
            GameObject item = Instantiate(selectedItemPrefab, spawnPosition.position, Quaternion.identity);
            item.transform.SetParent(bag.transform);
        }
    }

    public bool HasIllegalItems()
    {
        foreach (Transform child in bag.transform)
        {
            if (child.CompareTag("Illegal"))
            {
                return true;
            }
        }
        return false;
    }

    public void ClearItems()
    {
        foreach (Transform child in bag.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
