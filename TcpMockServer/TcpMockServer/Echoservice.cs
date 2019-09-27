using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Skatfærdig;

namespace TcpMockServer
{
    public class Echoservice
    {
        private TcpClient connectionSocket;
        Afgift nyafgift = new Afgift();

        public Echoservice(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        internal void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer;
            while (message != null && message != "")
            {
                Console.WriteLine("Client: " + message);
                if (message.Contains("elbil"))
                {
                    var number = Regex.Match(message, @"\d+").Value;

                    if (number != null)
                    {
                        int price = Convert.ToInt32(number);
                        if (message.Contains($"{number}k"))
                            price = price * 1000;

                        answer = "Afgift på ElBil: " + nyafgift.ElBilAfgift(price).ToString();
                    }
                    else
                    {
                        answer = "Please enter price eg. 200k";
                    }
                }
                else if (message.Contains("personbil"))
                {
                    var number = Regex.Match(message, @"\d+").Value;

                    if (number != null)
                    {
                        int price = Convert.ToInt32(number);
                        if (message.Contains($"{number}k"))
                            price = price * 1000;

                        answer = "Afgift på PersonBil: " + nyafgift.BilAfgift(price).ToString();
                    }
                    else
                    {
                        answer = "Please enter price eg. 200k";
                    }
                }
                else
                {
                    answer = "Please define PersonBil or Elbil and your price";
                }
                sw.WriteLine(answer);
                message = sr.ReadLine();
               

            }
            ns.Close();
            connectionSocket.Close();
        }
    }
}