using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnPlayerCountChange;

    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private Transform[] redGroupBornDummy = new Transform[3];
    [SerializeField]
    private Transform[] blueGroupBornDummy = new Transform[3];
    [SerializeField]
    private StartUI startUI = null;
    [SerializeField]
    private Color redGroupColor = Color.red;
    [SerializeField]
    private Color blueGroupColor = Color.blue;

    [SerializeField]
    private CountDownUI countDownUI = null;
    [SerializeField]
    private EndUI endUI = null;
    [SerializeField]
    private GameUIManager gameUIManager = null;

    private static GamePhase phase;

    private Dictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();

    private Dictionary<GroupType, int> score = new Dictionary<GroupType, int>();

    int playerCount = 0;

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += OnConnect;
        phase = GamePhase.WaitPlayer;
        countDownUI.OnCountDownFinishEvent += GameEnd;
    }

    private void Start()
    {
        OnPlayerCountChange += startUI.OnPlayerCountChange;
        startUI.Show();
        countDownUI.Init();
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
        var render = newPlayer.GetComponent<Renderer>();

        Vector3 pos = Vector3.zero;

        if(playerCount % 2 == 1)
        {
            data.Group = GroupType.Blue;
            int index = (int)(playerCount / 2);
            pos = blueGroupBornDummy[index].position;
            data.StartPostion = pos;
            render.material.color = blueGroupColor;
            phase = GamePhase.WaitPlayer;
        }
        else
        {
            data.Group = GroupType.Red;
            int index = (int)(playerCount / 2) -1;
            pos = redGroupBornDummy[index].position;
            data.StartPostion = pos;
            render.material.color = redGroupColor;
            phase = GamePhase.Ready;
        }

        newPlayer.transform.position = pos;

        var playerController = newPlayer.GetComponent<PlayerController>();
        players.Add(deviceID, playerController);

        OnPlayerCountChange?.Invoke(playerCount);
    }

    private void OnMessage(int from, JToken data)
    {
        Debug.Log("message: " + data);

        if(phase != GamePhase.OnBattle) { return; }

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

    public void GameStart()
    {
        phase = GamePhase.OnBattle;
        startUI.Hide();
        countDownUI.StartCountDown();
        var materialRandomCreate = GameObject.FindObjectOfType<MaterialRandomCreate>();
        materialRandomCreate?.StartCreate();
    }

    public void GameEnd()
    {
        phase = GamePhase.End;
        var materialRandomCreate = GameObject.FindObjectOfType<MaterialRandomCreate>();
        materialRandomCreate?.StopCreate();

        var machineManagers = GameObject.FindObjectsOfType<MachineManager>();

        var groupAPoint = gameUIManager.getTeamAScore();
        var groupBPoint = gameUIManager.getTeamBScore();

        if(groupAPoint > groupBPoint)
        {
            endUI.ShowWinner(GroupType.Blue);
        }
        else if (groupAPoint < groupBPoint)
        {
            endUI.ShowWinner(GroupType.Red);
        }
        else
        {
            endUI.ShowTie();
        }
    }

    public void Again()
    {
        countDownUI.Init();
        endUI.Hide();
        var machineManagers = GameObject.FindObjectsOfType<MachineManager>();
        foreach(var m in machineManagers)
        {
            m.ResetScore();
        }
        var materialRandomCreate = GameObject.FindObjectOfType<MaterialRandomCreate>();
        materialRandomCreate?.DestroyAllItem();

        foreach (var player in players)
        {
            player.Value.transform.position = player.Value.GetComponent<PlayerData>().StartPostion;
        }
        GameStart();
    }
}
