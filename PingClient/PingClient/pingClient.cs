using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace PingClient
{
    class pingClient
    {
        Random rnd = new Random();

        DateTime pingtimeSent;
        int PORT_NO;
        int timeOut = 200;
        bool zeroSent;
        string SERVER_IP = "127.0.0.1";
        TcpClient client;
        NetworkStream nwStream;
        internal pingClient(string ip, int port)
        {
            PORT_NO = port;
            SERVER_IP = ip;
            //client.ReceiveBufferSize = 4;
        }
        internal bool connect()
        {
            try
            {
                client = new TcpClient(SERVER_IP, PORT_NO);
                client.ReceiveTimeout = 100;
                client.SendTimeout = 100;
                nwStream = client.GetStream();
            }
            catch (Exception e)
            {
                return true;
            }
            return false;
        }
        internal int ping()
        {
            int data = rnd.Next(1, Int32.MaxValue);
            nwStream.ReadTimeout = timeOut;
            nwStream.WriteTimeout = timeOut;

            pingtimeSent = DateTime.Now;
            //first send
            if (sendData(data))
                return -1;

            //first read
            int returnData;
            returnData = readData();
            if (returnData == -1)
                return -1;

            //if wrong packet was returned, send '0' to tell the server to skip one, if '0' is returned, continue as normal
            if (returnData != data)
            {
                if (returnData == -1 || zeroSent)
                {
                    end();
                    connect();
                    return -1;
                }

                pingtimeSent = DateTime.Now;
                if (sendData(0))
                    return -1;

                returnData = readData();

                zeroSent = true;
                if (returnData != 0)
                {
                    end();
                    connect();
                    return -1;
                }
                else
                {
                    zeroSent = false;
                    return (int)(System.DateTime.Now - pingtimeSent).TotalMilliseconds;
                }
            }
            else
            {
                zeroSent = false;
                return (int)(System.DateTime.Now - pingtimeSent).TotalMilliseconds;
            }
        }
        internal int readData()
        {
            byte[] bytesToRead = new byte[4];
            try
            {
                nwStream.Read(bytesToRead, 0, 4);
            }
            catch (System.IO.IOException e)
            {
                return -1;
            }
            return BitConverter.ToInt32(bytesToRead, 0);
        }
        internal bool sendData(int data)
        {
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytesToSend);
            return sendData(BitConverter.GetBytes(data));
        }
        internal bool sendData(byte[] data)
        {
            try
            {
                nwStream.Write(data, 0, data.Length);
            }
            catch (System.IO.IOException e)
            {
                connect();
                return true;
            }
            return false;
        }
        internal void end()
        {
            if (client != null && client.Connected)
            {
                byte[] bytesToSend = BitConverter.GetBytes(-1);
                //if (BitConverter.IsLittleEndian)
                //                    Array.Reverse(bytesToSend);
                try { nwStream.Write(bytesToSend, 0, bytesToSend.Length); }
                catch (System.IO.IOException e) { }
                client.Close();
            }
        }
        internal void endServer()
        {
            if (client != null && client.Connected)
            {
                byte[] bytesToSend = BitConverter.GetBytes(-2);
                //                if (BitConverter.IsLittleEndian)
                //                    Array.Reverse(bytesToSend);
                try { nwStream.Write(bytesToSend, 0, bytesToSend.Length); }
                catch (System.IO.IOException e) { }
                client.Close();
            }
        }
        ~pingClient()
        {
            end();
        }
    }
}
