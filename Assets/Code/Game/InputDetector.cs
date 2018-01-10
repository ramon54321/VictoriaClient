using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictoriaShared.Timeline;
using VictoriaShared.Networking;

public class InputDetector : MonoBehaviour
{
	void Start ()
    {

    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                NetworkManager.GetInstance().SetUpServerConnection();
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                NetworkManager.GetInstance().ConnectToServer();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                NetworkManager.GetInstance().DisconnectFromServer();
            }
            if (Input.GetKeyUp(KeyCode.P))
            {
                DataBlock dataBlock = new DataBlock(1, 0, 0, "cl#Ping!");
                NetworkManager.GetInstance().SendDataBlock(dataBlock);
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                DataBlock dataBlock = new DataBlock(1, 0, 0, "cl");
                NetworkManager.GetInstance().SendDataBlock(dataBlock);
            }
        }
    }
}
