using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab = null;

    private Dictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();

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
        if (players.ContainsKey(deviceID))
        {
            return;
        }

        GameObject newPlayer = Instantiate<GameObject>(playerPrefab);
        var playerController = newPlayer.AddComponent<PlayerController>();
        players.Add(deviceID, playerController);
    }

    void OnMessage(int from, JToken data)
    {
        //TODO
    }
}
