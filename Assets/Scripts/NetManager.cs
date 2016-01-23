using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetManager : NetworkManager {

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = Vector3.right * conn.connectionId;
        GameObject player =(GameObject) Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);
        player.name = string.Format("Player {0}", numPlayers+1);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
