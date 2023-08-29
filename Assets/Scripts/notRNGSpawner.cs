using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class notRNGSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    //public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    //public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    //public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }







    // runner 라는 변수는 선언해서 써도 공유하고 그냥 가져다 써도(상위클래스의 변수) 공유하는건가?
    private NetworkRunner _runner;
    //private bool _mouseButton0;
    //private bool _mouseButton1;

    private void Update()
    {
        //_mouseButton0 = _mouseButton0 | Input.GetMouseButton(0);
        //_mouseButton1 = _mouseButton1 | Input.GetMouseButton(1);
    }

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }

    async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    //[SerializeField] private NetworkPrefabRef _playerPrefab;
    //private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    //UnitGenerator unitGenerator;
    Battle battle;
    int playerCount = 0;

    //[SerializeField] private NetworkPrefabRef[] units1, units2;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            //unitGenerator = FindObjectOfType<UnitGenerator>();
            battle = FindObjectOfType<Battle>();

            //unitGenerator.enabled = true;
            battle.enabled = true;

            

            print("Yes your honor");
            Players.instance.AddPlayers(player);
        } else if(runner.IsClient)
        {
            print("Maybe");
            Players.instance.AddPlayers(player);
        }

        //Players.instance.AddPlayers(player);

        // battle을 싱글톤으로 만들어야 하나?
        // battle 인스턴스가 여러개 만들어지고 있는건 아니겠지?
        // battle unitgenerator 언제만들어지는거야 대체
        // 서버 클라 실행 분리


        //if (runner.IsClient)
        //{
        //    print(" DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        //    //unitGenerator = FindObjectOfType<UnitGenerator>();
        //    //battle = FindObjectOfType<Battle>();

        //    //unitGenerator.InitialUnitSetUp(runner);
        //    // error
        //    print(unitGenerator);
        //    print(battle);


        //}

        //if (runner.IsServer)
        //{
        //    runner.Spawn(units1[0], new Vector3(-10, 0, 10), Quaternion.identity, player);
        //    print("server");
        //}
        //else if (runner.IsClient)
        //{
        //    runner.Spawn(units2[1], new Vector3(10, 0, 10), Quaternion.identity, player);
        //    print("cla");
        //}













        //// Create a unique position for the player
        //Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
        //NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
        //// Keep track of the player avatars so we can remove it when they disconnect
        //_spawnedCharacters.Add(player, networkPlayerObject);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {





        //// Find and remove the players avatar
        //if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        //{
        //    runner.Despawn(networkObject);
        //    _spawnedCharacters.Remove(player);
        //}
    }

    //public void OnInput(NetworkRunner runner, NetworkInput input)
    //{
    //    var data = new NetworkInputData();

    //    if (Input.GetKey(KeyCode.W))
    //        data.direction += Vector3.forward;

    //    if (Input.GetKey(KeyCode.S))
    //        data.direction += Vector3.back;

    //    if (Input.GetKey(KeyCode.A))
    //        data.direction += Vector3.left;

    //    if (Input.GetKey(KeyCode.D))
    //        data.direction += Vector3.right;

    //    if (_mouseButton0)
    //        data.buttons |= NetworkInputData.MOUSEBUTTON1;
    //    _mouseButton0 = false;

    //    if (_mouseButton1)
    //        data.buttons |= NetworkInputData.MOUSEBUTTON2;
    //    _mouseButton1 = false;

    //    input.Set(data);
    //}

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }
}