using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //소켓뚫기
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            //목적지 주소. 서버랜카드ip
            serverSocket.Connect(serverEndPoint);
            //서버에 전화하고 들어가기. 

            byte[] buffer;

            int cal = 100 + 200;
            String message = $"안녕하세요.{cal}";

            buffer = Encoding.UTF8.GetBytes(message);

            int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);

            //블럭킹
            byte[] buffer2 = new byte[1024];
            int RecvLength = serverSocket.Receive(buffer2);

            //Console.WriteLine(Encoding.UTF8.GetString(buffer2));
            serverSocket.Close();

        }
    }
}
