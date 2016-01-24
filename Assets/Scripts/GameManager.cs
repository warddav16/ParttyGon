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

    [SyncVar]
    private int numInfected = 0;

    [SyncVar]
    private int currentRound = 0;
    public int GetCurrentRound() { return currentRound; }

    public int PlayerInfectedPointsAward = 1;
    public int RoundSurvivalPointsAward = 5;

    [SyncVar]
    private float roundTimer = 0;
    [SyncVar]
    private bool isInRound = false;

    private GameObject[] players;

    private Dictionary<string, int> scores = new Dictionary<string, int>();
    [Command]
    public void CmdRegisterPlayer(string id)
    {
        scores.Add(id, 0);
    }

    public int GetScore(string id)
    {
        int outScore = -1;
        if (scores.TryGetValue(id, out outScore))
        {
            return outScore;
        }
        return -1;
    }
    private void SetScore(string player, int score)
    {
        scores[player] = score;
    }

    public Transform[] SpawnPoints;

    [SyncVar]
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
        players = GameObject.FindObjectsOfType<CustomCharacterController>().Select(x => x.gameObject).ToArray();

        if (!isServer)
            return;

        CmdRoundStart();
    }

    [Command]
    void CmdRoundStart()
    {
        if (++currentRound > TotalRounds)
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
    [Command]
    public void CmdPlayerInfected(string awardPlayer)
    {
        RpcSendUpdatedScores(awardPlayer, PlayerInfectedPointsAward);
        if (++numInfected >= players.Length)
        {
            CmdRoundEnd();
        }
    }
    [ClientRpc]
    private void RpcSendUpdatedScores(string player, int score)
    {
        SetScore(player, GetScore(player) + score);
    }
}
