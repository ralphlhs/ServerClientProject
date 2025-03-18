using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var json = new JObject();
            json.Add("message", 
                "https://discordapp.com/channels/1329683146446471231/1329684212747599924/1351346155908497509");


            Socket listensocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp); // OS에 소켓구멍 뚫기.
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);//IPAddress.Parse(000,000,000,00), 4000
            //IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000); <- 강사님 서버
            //프로그램이 랜카드와 통신하는 명령 // port는 문지기
            listensocket.Bind(listenEndPoint);
            //랜카드와 프로그램 연결.

            listensocket.Listen(10);
            //클라이언트 동시에 접속했을때 줄세우는 명수(10개까지) 똑똑!

            bool isRunning = true;
            while (isRunning)
            {
                //동기방식, 블록킹
                Socket clientSocket = listensocket.Accept();//들어와

                byte[] buffer = new byte[1024];
                int RecvLength = clientSocket.Receive(buffer);//받거나
                string a = Encoding.UTF8.GetString(buffer);

                if (RecvLength <= 0)
                {
                    //close
                    //error
                    isRunning = false;
                }

                string b = "https://discordapp.com/channels/1329683146446471231/1329684212747599924/1351346155908497509";

                byte[] buffer2 = new byte[1024];
                buffer2 = Encoding.UTF8.GetBytes(b);
                int SendLength = clientSocket.Send(buffer2);//주거나

                if (SendLength <= 0)
                {
                    isRunning = false;
                }
     
                Console.WriteLine(a);
                //KEEP ALIVE TIME
                clientSocket.Close();
            }

            listensocket.Close();
        }
    }
}
