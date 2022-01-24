﻿using LiteNetLib;
using LiteNetLib.Utils;
using Tetris.Game;
using Tetris.Game.InGame;

namespace Tetris.Multiplayer.Network.Packets
{
    public class EndGamePacket : Packet
    {
        public EndGamePacket(int id) : base(id){}

        protected override void SendPacket()
        {
            dataWriter = new NetDataWriter();
            dataWriter.Put(PacketID);
            base.SendPacket();
        }

        protected override void RunPacket(NetPacketReader packetReader)
        {
            TetrisBoard.Instance.AddBlockToBoard(7,0,Globals.LowestY);
            base.RunPacket(packetReader);
        }
    }
}