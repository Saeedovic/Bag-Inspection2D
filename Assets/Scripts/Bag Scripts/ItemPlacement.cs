using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public GameObject bag;
    public Transform[] spawnPositions;
    public int random;
    public bool islocal;
    public NetworkComponent networkComponent;
    public int index;
    public List<int> intsss = new List<int>();
    public bool cleared;


    public void Start()
    {
        networkComponent = FindObjectOfType<NetworkComponent>();
        islocal = true;
    }

    public IEnumerator SendPackets()
    {

        for (int i = 0; i < intsss.Count; i++)
        {
            NetworkManager.instance.Send(new RandomPacket(NetworkManager.instance.playerData,
                      networkComponent.GameObjectID,
                     intsss[i]).Serialize());
            Debug.Log("SENTTTTT" + random);
            yield return new WaitForSeconds(0.25f);
        }
        intsss.Clear();

    }
    public void PlaceItems(int random)
    {
        Debug.Log("Placed items calleddd");

        if (!cleared)
        {

            ClearItems();
            cleared = true;
        }

        if (islocal)
        {
            foreach (Transform spawnPosition in spawnPositions)
            {
                random = Random.Range(0, itemPrefabs.Count);
                GameObject selectedItemPrefab = itemPrefabs[random];
                Vector3 newPosition = new Vector3(spawnPosition.position.x, spawnPosition.position.y, -1);
                GameObject item = Instantiate(selectedItemPrefab, newPosition, Quaternion.identity);
                item.transform.SetParent(bag.transform);

                intsss.Add(random);

            }

            StartCoroutine(SendPackets());

        }
        else
        {
            GameObject selectedItemPrefab = itemPrefabs[random];
            Vector3 newPosition = new Vector3(spawnPositions[index].position.x, spawnPositions[index].position.y, -1);
            GameObject item = Instantiate(selectedItemPrefab, newPosition, Quaternion.identity);
            item.transform.SetParent(bag.transform);
            index++;
            if (index >= spawnPositions.Length)
            {
                index = 0;
                cleared = false;
            }

            islocal = true;
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
            if (child.CompareTag("Illegal") || child.CompareTag("Legal"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
