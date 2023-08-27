using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcRandomPacket : BasePacket
{
    public int random { get; private set; }
    public NpcRandomPacket() : base()
    {
        random = 0;
    }

    public NpcRandomPacket(PlayerData player, string GameObjectID, int random)
        : base(player, GameObjectID, PackType.NpcRandom)
    {
        this.random = random;
    }
    public byte[] Serialize()
    {
        BeginSerialize();
        bw.Write(random);

        return EndSerialize();
    }

    public new NpcRandomPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        random = br.ReadInt32();


        return this;
    }
}
