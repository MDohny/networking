using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class ServerModel
    {
        private Task listeningProcess;
        private bool listeningCondition;

        private IPAddress address;
        private int portNumber;

        private TcpListener tcpListener;
        private GuessNumberService guessNumberService;

        public ServerModel(IPAddress address, int portNumber)
        {
            this.address = address;
            this.portNumber = portNumber;
        }

        public void SetService(GuessNumberService guessNumberService)
        {
            this.guessNumberService = guessNumberService;
        }

        public void StartListening()
        {
            listeningCondition = true;
            guessNumberService = new GuessNumberService();

            tcpListener = new TcpListener(address, portNumber);
            tcpListener.Start();

            listeningProcess = Task.Run(() =>
            {
                byte[] bytes = new byte[256];
                string data = null;
                try
                {
                    while (listeningCondition)
                    {

                        TcpClient client = tcpListener.AcceptTcpClient();
                        data = null;

                        NetworkStream stream = client.GetStream();

                        int i;

                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            data = Encoding.ASCII.GetString(bytes, 0, i);

                            GuessNumberRequestData requestData = DataConvertor.FromString<GuessNumberRequestData>(data);
                            GuessNumberResponseData responseData = guessNumberService.ProcessRequest(requestData);
                            data = DataConvertor.ToString<GuessNumberResponseData>(responseData);

                            byte[] msg = Encoding.ASCII.GetBytes(data);

                            stream.Write(msg, 0, msg.Length);
                        }

                        client.Close();
                    }
                }
                finally
                {
                    tcpListener.Stop();
                }
            });
        }

        public void StopListening()
        {
            listeningCondition = false;
            TcpClient tcpClient = new TcpClient(address.ToString(), portNumber);
            NetworkStream stream = tcpClient.GetStream();
            stream.Close();
            tcpClient.Close();
            guessNumberService = null;
            listeningProcess.Wait();
        }
    }
}
