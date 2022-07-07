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
              return msg;
          }
      }
  }
