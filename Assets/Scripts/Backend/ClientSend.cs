using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.tcp.SendData(_packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.Instance.netID);

            SendTCPData(_packet);
        }
    }

    public static void RegisterRequest(string username)
    {
        using (Packet _packet = new Packet((int)ClientPackets.registerRequest))
        {
            _packet.Write(username);

            SendTCPData(_packet);
        }
    }

    public static void VerificationRequest(int id, string password)
    {
        using (Packet _packet = new Packet((int)ClientPackets.verificationRequest))
        {
            byte[] _salt = Hasher.GenerateSalt();
            byte[] _hash = Hasher.HashPassword(password, _salt);

            _packet.Write(id);
            _packet.Write(Hasher.Encode(_salt));
            _packet.Write(Hasher.Encode(_hash));

            SendTCPData(_packet);
        }
    }

    public static void ChangeAvatarRequest(int id, int avatar)
    {
        using (Packet _packet = new Packet((int)ClientPackets.avatarRequest))
        {
            _packet.Write(id);
            _packet.Write(avatar);

            SendTCPData(_packet);
        }
    }

    public static void CharacterCreationRequest(int id, string name, string gender)
    {
        using (Packet _packet = new Packet((int)ClientPackets.characterCreationRequest))
        {
            _packet.Write(id);
            _packet.Write(name);
            _packet.Write(gender);

            SendTCPData(_packet);
        }
    }

    public static void AccountDataRequest(int id, string username)
    {
        using (Packet _packet = new Packet((int)ClientPackets.accountDataRequest))
        {
            _packet.Write(id);
            _packet.Write(username);

            SendTCPData(_packet);
        }
    }

    public static void LogInRequest(string username)
    {
        using (Packet _packet = new Packet((int)ClientPackets.logInRequest))
        {
            _packet.Write(username);

            SendTCPData(_packet);
        }
    }

    public static void LogIn(string username, string hash)
    {
        using (Packet _packet = new Packet((int)ClientPackets.logIn))
        {
            _packet.Write(username);
            _packet.Write(hash);

            SendTCPData(_packet);
        }
    }
}
