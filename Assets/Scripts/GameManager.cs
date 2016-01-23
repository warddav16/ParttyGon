using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public float RoundTime = 120.0f;
    public int TotalRounds = 2;
    public int MaxPlayers = 8;
    private int numInfected = 0;
    private int currentRound = 0;

    public int PlayerInfectedPointsAward = 1;
    public int RoundSurvivalPointsAward = 5;

    private float roundTimer = 0;
    private bool isInRound = false;

    private GameObject[] players;
    private int[] scores;

    public Transform[] SpawnPoints;

    private int prevInfectedPlayer = -1;

    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (!isServer)
            return;

        players = GameObject.FindObjectsOfType<CustomCharacterController>().Select(x => x.gameObject).ToArray();
        scores = new int[players.Length];
        CmdRoundStart();
    }

    [Command]
    void CmdRoundStart()
    {
        if(++currentRound > TotalRounds)
        {
            CmdEndGame();
            return;
        }
        numInfected = 0;
        roundTimer = 0;
        LinkedList<Transform> availSpawns = new LinkedList<Transform>(SpawnPoints);
        for (int i = 0; i < players.Length; ++i)
        {
            Transform loc = availSpawns.ElementAt(Random.Range(0, availSpawns.Count));
            availSpawns.Remove(loc);
            players[i].GetComponent<CustomCharacterController>().RpcSetPosition(loc.position);
            players[i].GetComponent<Infection>().RpcSetInfect(false);
            players[i].GetComponent<CustomCharacterController>().RpcSetCanMove(true);
        }

        CmdInfectRandomPlayer();
        isInRound = true;
    }
    
    [Command]
    void CmdRoundEnd()
    {
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].GetComponent<CustomCharacterController>().RpcSetCanMove(false);
        }
        isInRound = false;
    }

    [Command]
    void CmdEndGame()
    {
        
    }

    void Update()
    {
        if (!isServer)
            return;

        if (isInRound)
        {
            roundTimer += Time.deltaTime;
            if (roundTimer >= RoundTime)
            {
                CmdRoundEnd();
            }
        }
    }
    
    [Command]
    public void CmdPlayerInfected()
    {
        if (++numInfected >= players.Length)
        {
            CmdRoundEnd();
        }
    }

    [Command]
    void CmdInfectRandomPlayer()
    {
        int playerToInfect = -1;
        do
        {
            playerToInfect = Random.Range(0, players.Length);
        } while (playerToInfect == prevInfectedPlayer);
        players[playerToInfect].GetComponent<Infection>().RpcSetInfect(players[playerToInfect]);
        numInfected++;
        prevInfectedPlayer = playerToInfect;
    }    
}
