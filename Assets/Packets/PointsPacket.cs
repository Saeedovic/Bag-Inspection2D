using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPacket : BasePacket
{
    public int points { get; private set; }

    public PointsPacket() : base()
    {
        points = 0;
    }

    public PointsPacket(PlayerData player, string GameObjectID, int points)
        : base(player, GameObjectID, PackType.Points)
    {
        this.points = points;
    }

    public byte[] Serialize()
    {
        BeginSerialize();
        bw.Write(points);

        return EndSerialize();
    }

    public new PointsPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        points = br.ReadInt32();

        return this;
    }
}