using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMovementPacket : BasePacket
{
    public BagData bag { get; private set; }

    public BagMovementPacket()
    : base()
    {
        bag = new BagData(new Vector3(), new Vector3(), 0f);
    }

    public BagMovementPacket(PlayerData player, string GameObjectID, BagData bag)
        : base(player, GameObjectID, PackType.BagMovement)
    {
        this.bag = bag;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(bag.startPos.x);
        bw.Write(bag.startPos.y);
        bw.Write(bag.startPos.z);
        bw.Write(bag.endPos.x);
        bw.Write(bag.endPos.y);
        bw.Write(bag.endPos.z);
        bw.Write(bag.speed);

        return EndSerialize();
    }

    public new BagMovementPacket Deserialize(byte[] buffer)
    {
        BeginDeserialize(buffer);

        Vector3 startPos = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Vector3 endPos = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        float speed = br.ReadSingle();

        bag = new BagData(startPos, endPos, speed);

        EndDeserialize();

        return this;
    }
}