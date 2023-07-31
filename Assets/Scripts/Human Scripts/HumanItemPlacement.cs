using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItemPlacement : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public GameObject npc;
    public Transform[] spawnPositions;

    public void PlaceItems()
    {
        ClearItems();

        foreach (Transform spawnPosition in spawnPositions)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject selectedItemPrefab = itemPrefabs[randomIndex];
            Vector3 newPosition = new Vector3(spawnPosition.position.x, spawnPosition.position.y, -1);
            GameObject item = Instantiate(selectedItemPrefab, newPosition, Quaternion.identity);
            item.transform.SetParent(npc.transform);
        }
    }

    public bool HasIllegalItems()
    {
        foreach (Transform child in npc.transform)
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
        foreach (Transform child in npc.transform)
        {
            if (child.CompareTag("Illegal") || child.CompareTag("Legal"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
