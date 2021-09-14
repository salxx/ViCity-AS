using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Mirror.SimpleWeb;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Unity.Jobs;

public class ServerOnly : MonoBehaviour
{
    public SimpleWebTransport transport;
    public NetworkManager networkManager;
    static NetworkManager networkManagerStatic;
    JobHandle monitoringJobHandle;
    MonitoringJob monitoringJob;

    private void OnDestroy()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer && !Application.isEditor) // server side
        {
            if (!monitoringJobHandle.IsCompleted)
            {
                monitoringJob.Interrupt();
                monitoringJobHandle.Complete();
            }
        }
    }
    void Start()
    {
        networkManagerStatic = networkManager;
        ushort port = 8000;
        if (Application.platform != RuntimePlatform.WebGLPlayer && !Application.isEditor) // server side
        {
            string[] args = System.Environment.GetCommandLineArgs();
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    if (arg.Equals("-port"))
                    {
                        port = ushort.Parse(args[i + 1]);
                        break;
                    }
                }
            }
            transport.port = port;
            networkManager.enabled = true;
            StartCoroutine(DelayedServerStart());
            StartCoroutine(MonitoringCoroutine(port));
        } else
        {
            TcpClient tcpClient = new TcpClient("localhost", 8008);
            Byte[] data = new Byte[256];
            String responseData = String.Empty;
            NetworkStream stream = tcpClient.GetStream();
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            stream.Close();
            tcpClient.Close();
            port = ushort.Parse(responseData);
            Debug.Log("Received Port: " + port);

            transport.port = port;
            networkManager.enabled = true;
            StartCoroutine(DelayedClientStart());
        }
    }

    IEnumerator DelayedServerStart()
    {
        yield return null;
        networkManager.StartServer();
    }

    IEnumerator DelayedClientStart()
    {
        yield return null;
        networkManager.StartClient();
    }

    IEnumerator MonitoringCoroutine(int port)
    {
        monitoringJob = new MonitoringJob();
        monitoringJob.port = port - 1000;
        monitoringJobHandle = monitoringJob.Schedule();
        while (!monitoringJobHandle.IsCompleted)
        {
            yield return null;
        }
        monitoringJobHandle.Complete();
    }

    static TcpListener server;

    private struct MonitoringJob : IJob
    {
        public int port;

        public void Execute()
        {
            ControlEndpoint(port);
        }

        public void ControlEndpoint(Int32 port)
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                //Byte[] bytes = new Byte[256];
                //String data = null;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    //data = null;
                    NetworkStream stream = client.GetStream();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("" + networkManagerStatic.numPlayers);
                    stream.Write(msg, 0, msg.Length);
                    /*int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        data = data.ToUpper();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("" + networkManagerStatic.spawnPrefabs.Count);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }*/
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }

        internal void Interrupt()
        {
            server.Stop();
        }
    }

    
}
