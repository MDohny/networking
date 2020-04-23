using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace networking.Models
{
    public class ClientModel
    {
        private TcpClient tcpClient;
        private string server;
        private int portNumber;

        public static ClientModel Connect(string server, int portNumber)
        {
            ClientModel client = new ClientModel()
            {
                server = server,
                portNumber = portNumber
            };
            return client;
        }
        public string SendData(string data)
        {
            
            byte[] _data = Encoding.ASCII.GetBytes(data);
            string responseData = string.Empty;
            try
            {
                tcpClient = new TcpClient(server, portNumber);
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(_data, 0, _data.Length);

                _data = new byte[256];

                int bytes = stream.Read(_data, 0, _data.Length);
                responseData = Encoding.ASCII.GetString(_data, 0, bytes);

                stream.Close();
            }
            finally
            {
                tcpClient.Close();
            }
            return responseData;
        }
    }
}
