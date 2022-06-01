using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class network : NetworkManager 
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    GameObject asteroide;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        //if (numPlayers == 2)
        //{
        //    asteroide = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Asteroid"));
        //    NetworkServer.Spawn(asteroide);
        //}
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        //if (asteroide != null)
        //{
        //    NetworkServer.Destroy(asteroide);
        //}

        base.OnServerDisconnect(conn);
    }
}
