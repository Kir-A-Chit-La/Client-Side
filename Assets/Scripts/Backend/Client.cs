using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client Instance;
    public static int DataBufferSize = 4096;

    [SerializeField] private string _ip = "192.168.0.250";
    [SerializeField] private int _port = 26950;
    public int _netID;
    private bool _isConnected = false;
    public TCP tcp;

    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        tcp = new TCP();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient Socket;
        private NetworkStream _stream;
        private Packet _receivedData;
        private byte[] _receiveBuffer;

        public void Connect()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = DataBufferSize,
                SendBufferSize = DataBufferSize
            };

            _receiveBuffer = new byte[DataBufferSize];
            Socket.BeginConnect(Instance._ip, Instance._port, ConnectCallback, Socket);
        }

        public bool SocketConnected()
        {
            if(Socket.Connected && Socket.Available == 0)
            {
                Instance._isConnected = true;
                return true;
            }
            else
            {
                Instance._isConnected = false;
                return false;
            }
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            Socket.EndConnect(_result);

            if(!Socket.Connected)
            {
                return;
            }
            _stream = Socket.GetStream();

            _receivedData = new Packet();

            _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if(Socket != null)
                {
                    _stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch(Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = _stream.EndRead(_result);
                if(_byteLength <= 0)
                {
                    Instance.Disconnect();
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(_receiveBuffer, _data, _byteLength);

                _receivedData.Reset(HandleData(_data));
                _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;
            _receivedData.SetBytes(_data);
            if(_receivedData.UnreadLength() >= 4)
            {
                _packetLength = _receivedData.ReadInt();
                if(_packetLength <= 0)
                {
                    return true;
                }
            }

            while(_packetLength > 0 && _packetLength <= _receivedData.UnreadLength())
            {
                byte[] _packetBytes = _receivedData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });
                _packetLength = 0;
                if(_receivedData.UnreadLength() >= 4)
                {
                    _packetLength = _receivedData.ReadInt();
                    if(_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }
            if(_packetLength <= 1)
            {
                return true;
            }
            return false;
        }

        private void Disconnect()
        {
            Instance.Disconnect();

            _stream = null;
            _receivedData = null;
            _receiveBuffer = null;
            Socket = null;
        }
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandle.Welcome },
            { (int)ServerPackets.registrationResult, ClientHandle.RegistrationResult },
            { (int)ServerPackets.verificationResult, ClientHandle.VerificationResult },
            { (int)ServerPackets.characterCreationResult, ClientHandle.CharacterCreationResult },
            { (int)ServerPackets.accountData, ClientHandle.AccountData },
            { (int)ServerPackets.accountSalt, ClientHandle.AccountSalt },
            { (int)ServerPackets.logInResult, ClientHandle.LogInResult }
        };
        Debug.Log("Initialized packets.");
    }

    private void Disconnect()
    {
        if(_isConnected)
        {
            _isConnected = false;
            tcp.Socket.Close();

            Debug.Log("Disconnected from server.");
        }
    }
}
