using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BasePacket
{
    protected MemoryStream writeStream;
    protected BinaryWriter bw;

    protected MemoryStream readStream;
    protected BinaryReader br;

    public PlayerData player { get; private set; }
    public string GameObjectID;

    public enum PackType
    {
        None,
        ItemPlacement,
        BagMovement,



       
    }

    public PackType packType { get; private set; }

    public BasePacket()
    {
        player = new PlayerData("", "");
        GameObjectID = "";
        packType = PackType.None;
    }

    protected BasePacket(PlayerData player, string GameObjectID, PackType packType)
    {
        this.player = player;
        this.GameObjectID = GameObjectID;
        this.packType = packType;
    }

    protected void BeginSerialize()
    {
        writeStream = new MemoryStream();
        bw = new BinaryWriter(writeStream);

        bw.Write(player.ID);
        bw.Write(player.Name);
        bw.Write(GameObjectID);

        bw.Write((int)packType);
    }

    protected byte[] EndSerialize()
    {
        return writeStream.ToArray();
    }

    public BasePacket Deserialize(byte[] buffer)
    {
        BeginDeserialize(buffer);
        EndDeserialize();
        return this;
    }

    protected void BeginDeserialize(byte[] buffer)
    {
        readStream = new MemoryStream(buffer);
        br = new BinaryReader(readStream);

        player = new PlayerData(br.ReadString(), br.ReadString());
        GameObjectID = br.ReadString();
        packType = (PackType)br.ReadInt32();
    }
    protected void EndDeserialize()
    {
        
    }
}