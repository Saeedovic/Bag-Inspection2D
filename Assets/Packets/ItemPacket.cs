using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPacket : BasePacket
{
    public List<ItemData> items { get; private set; }

    public ItemPacket()
        : base()
    {
        items = new List<ItemData>();
    }

    public ItemPacket(PlayerData player, string GameObjectID, List<ItemData> items)
        : base(player, GameObjectID, PackType.ItemPlacement)
    {
        this.items = items;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(items.Count);

        foreach (var item in items)
        {
            bw.Write(item.itemName);
            bw.Write(item.position.x);
            bw.Write(item.position.y);
            bw.Write(item.position.z);
       
        }

        return EndSerialize();
    }

    public new ItemPacket Deserialize(byte[] buffer)
    {
        BeginDeserialize(buffer);

        int itemCount = br.ReadInt32();
        items = new List<ItemData>();

        for (int i = 0; i < itemCount; i++)
        {
            string itemName = br.ReadString();
            Vector3 position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            ItemData item = new ItemData(itemName, position);
            items.Add(item);
        }

        EndDeserialize();

        return this;
    }
}
