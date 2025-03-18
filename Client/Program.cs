using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //소켓뚫기
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            //목적지 주소. 서버랜카드ip
            clientSocket.Connect(listenEndPoint);
            //서버에 전화하고 들어가기. 

            string jsonString = "{\"message\" : \"안녕하세요\"}";
            byte[] message = Encoding.UTF8.GetBytes(jsonString);
            int SendLength = clientSocket.Send(message);

        }


        static void Mywork()
        {
            var json = new JObject();
            json.Add("message", "안녕하세요.");
            Console.WriteLine(json);


            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //소켓뚫기
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            //목적지 주소. 서버랜카드ip
            serverSocket.Connect(serverEndPoint);
            //서버에 전화하고 들어가기. 

            byte[] buffer;

            int cal = 100 + 200;
            String message = $"안녕하세요.{cal}";
            String message2 = json.ToString();

            buffer = Encoding.UTF8.GetBytes(message2);

            int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);

            //블럭킹
            byte[] buffer2 = new byte[1024];
            int RecvLength = serverSocket.Receive(buffer2);

            Console.WriteLine(Encoding.UTF8.GetString(buffer2));
            serverSocket.Close();
        }
        static void FileClientProcess()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 4000);

            serverSocket.Connect(endPoint);

            string path = "./e.webp";
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            //byte[] imageBytes = new byte[100000];

            bool isCompleted = false;
            while (!isCompleted)
            {
                byte[] receiveBuffer = new byte[1024];
                int receiveSize = serverSocket.Receive(receiveBuffer);

                if (receiveSize != 1024)
                {
                    bw.Write(receiveBuffer);
                    isCompleted = true;
                }
                else
                {
                    bw.Write(receiveBuffer);
                }
            }

            serverSocket.Close();
        }
    }
}

