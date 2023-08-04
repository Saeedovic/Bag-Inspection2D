using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagMovementPacket : BasePacket
{
    public Vector3 Pos { get; private set; }

    public BagMovementPacket() :
        base()
    {
        Pos = Vector3.zero;
        
    }

    public BagMovementPacket(
        PlayerData player,
        string GameObjectID,
        Vector3 Pos
        ) : base(player, GameObjectID, PackType.BagMovement)
    {
        this.Pos = Pos;
       
    }

    public byte[] Serialize()
    {

        BeginSerialize();

        bw.Write(Pos.x);
        bw.Write(Pos.y);
        bw.Write(Pos.z);

       

        return EndSerialize();
    }

    public new BagMovementPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        Pos = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
       

        return this;
    }
}