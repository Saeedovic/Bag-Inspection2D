using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomPacket : BasePacket
{
    public int random { get; private set; }
    public RandomPacket() : base()
    {
        random = 0;
    }

    public RandomPacket(PlayerData player, string GameObjectID, int random)
        : base(player, GameObjectID, PackType.Random)
    {
        this.random = random;
    }
    public byte[] Serialize()
    {
        BeginSerialize();
        bw.Write(random);

        return EndSerialize();
    }

    public new RandomPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);


        random = br.ReadInt32();


        return this;
    }
}
