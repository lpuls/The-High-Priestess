using Hamster;
using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class ConnectSuccessMessage : NetMessage {

    public override int NetMessageID { get { return 1; } }

    public int Value1 = 0;
    public int Value2 = 0;

    public override Packet ToPacket(INetDevice netDevice) {
        Packet packet = netDevice.Malloc(16);
        packet.WriteInt32(1);  // 写入模块ID
        packet.WriteInt32(NetMessageID);
        packet.WriteInt32(Value1);
        packet.WriteInt32(Value2);
        return packet;
    }
}

public class NetMessageModule : NetModule {
    public override int GetModuleID() {
        return 1;
    }

    public override void OnReceiveMessage(Packet p) {
        int messageID = p.ReadInt32();
        int messageValue1 = p.ReadInt32();
        int messageValue2 = p.ReadInt32();
        UnityEngine.Debug.Log("Type " + messageID + " Value1 " + messageValue1 + " Value2 " + messageValue2);
    }

    public override void OnSendMessageFaile(Packet p, SocketError error) {
    }
}

public class Test : MonoBehaviour {


    //private NetDevice _netDevice = new NetDevice();
    public SaveHelper SaveHelper = null;
    // public BlackboardForSave Blackboard = new BlackboardForSave();



    // Use this for initialization
    void OnGUI() {
        //if (GUILayout.Button("Init")) {
        //    SaveHelper = new SaveHelper(@"H:\Dev\Demo\HighPriestess\Dava.sav", 0, Blackboard);
        //}
        //if (GUILayout.Button("Save")) {
        //    Blackboard.SetValue(1, 1234);
        //    Blackboard.SetValue(2, 5678);
        //    Blackboard.SetValue(4, 9101112);
        //    SaveHelper.Save();
        //}
        //if (GUILayout.Button("Load")) {
        //    Blackboard.Clean();
        //    SaveHelper.Load();
        //}

        //if (!_netDevice.IsValid && GUILayout.Button("Connect")) {
        //    _netDevice.RegistModule(new NetMessageModule());
        //    _netDevice.Connect("127.0.0.1", 8080);

        //}
        //if (_netDevice.IsValid && GUILayout.Button("Send")) {
        //    ConnectSuccessMessage message = new ConnectSuccessMessage {
        //        Value1 = 1,
        //        Value2 = 2
        //    };
        //    _netDevice.SendMessage(message);
        //}
        //if (_netDevice.IsValid && GUILayout.Button("Close")) {
        //    _netDevice.Close();
        //}
    }

}
