using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;


public class readSocket : MonoBehaviour {
      // Use this for initialization
      TcpListener listener;
      String msg = "";
      public void Start () {
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        listener=new TcpListener (localAddr, 55001);
        listener.Start ();
        print ("is listening");
      }
      // Update is called once per frame
      public String Update () {
        if (!listener.Pending())
          {
            msg= "NoGesto";
            return msg;
          } 
          else 
          {
              print ("socket comes");
              TcpClient client = listener.AcceptTcpClient ();
              NetworkStream ns = client.GetStream ();
              StreamReader reader = new StreamReader (ns);
              msg = reader.ReadToEnd();
              print (msg);
              switch(msg){
                case "1":
                  msg = "WaveIn";
                  break;
                case "2":
                  msg = "WaveOut";
                  break;
                case "3":
                  msg = "Fist";
                  break;
                case "4":
                  msg = "Open";
                  break;
                case "5":
                  msg = "Pinch";
                  break;
                case "6":
                  msg = "Up";
                  break;
                case "7":
                  msg = "Down";
                  break;
                case "8":
                  msg = "Left";
                  break;
                case "9":
                  msg = "Right";
                  break;
                case "10":
                  msg = "Forward";
                  break;
                case "11":
                  msg = "Backward";
                  break;
                case "0":
                  msg = "NoGesto";
                  break;
              }
              return msg;
          }
      }
  }
