﻿using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private Transform[] redGroupBornDummy = new Transform[3];
    [SerializeField]
    private Transform[] blueGroupBornDummy = new Transform[3];

    private Dictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();

    private Dictionary<GroupType, int> score = new Dictionary<GroupType, int>();

    int playerCount = 0;

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += OnConnect;
    }

    private void Start()
    {
       ///TODO: show wiat UI 
    }

    private void OnReady(string code)
    {
        Debug.Log($"OnReady:{code}");

        List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
        foreach (int deviceID in connectedDevices)
        {
            AddNewPlayer(deviceID);
        }
    }

    void OnConnect(int device)
    {
        AddNewPlayer(device);
    }

    private void AddNewPlayer(int deviceID)
    {
        if(playerCount >= 6) { return; }

        if (players.ContainsKey(deviceID))
        {
            return;
        }

        playerCount++;

        GameObject newPlayer = Instantiate<GameObject>(playerPrefab);
        var data = newPlayer.AddComponent<PlayerData>();

        Vector3 pos = Vector3.zero;

        if(playerCount % 2 == 1)
        {
            data.Group = GroupType.Blue;
            int index = (int)(playerCount / 2);
            pos = blueGroupBornDummy[index].position;
        }
        else
        {
            data.Group = GroupType.Red;
            int index = (int)(playerCount / 2) -1;
            pos = redGroupBornDummy[index].position;
        }

        newPlayer.transform.position = pos;

        var playerController = newPlayer.GetComponent<PlayerController>();
        players.Add(deviceID, playerController);
    }

    private void OnMessage(int from, JToken data)
    {
        Debug.Log("message: " + data);

        if (!players.TryGetValue(from, out var player)) { return; }

        if(data["action"] == null) { return; }

        var key = data["action"].ToString();

        switch (key)
        {
            case "left":
                player.StartGoLeft();
                break;
            case "left-up":
                player.StopGoLeft();
                break;
            case "right":
                player.StartGoRight();
                break;
            case "right-up":
                player.StopGoRight();
                break;
            case "down":
                player.StartGoDown();
                break;
            case "down-up":
                player.StopGoDown();
                break;
            case "up":
                player.StartGoUp();
                break;
            case "up-up":
                player.StopGoUp();
                break;
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onReady -= OnReady;
            AirConsole.instance.onConnect -= OnConnect;
        }
    }

    public static void AddPoint(int point, GroupType group)
    {
    }
}
