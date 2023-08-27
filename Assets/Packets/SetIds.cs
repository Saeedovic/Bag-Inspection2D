using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SetIds : BasePacket
{

    public string bagId { get; private set; }
    public string humanId { get; private set; }


    public SetIds() :
            base()
    {
        bagId = "";
        humanId = "";

    }

    public SetIds(
        PlayerData player,
        string GameObjectID,string bagId, string HumanId
        ) : base(player, GameObjectID, PackType.SetIds)
    {
        this.bagId = bagId;
        this.humanId = humanId;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(bagId);
        bw.Write(humanId);
        



        return EndSerialize();
    }

    public new SetIds Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        bagId = br.ReadString();
        humanId = br.ReadString();


        return this;
    }

}

