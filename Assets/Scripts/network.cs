using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class network : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    GameObject Spawn_Manager;
    GameObject Spawn_Manager2;
    float timeS;
    float timeS2;
    bool activo = false;
    //   public GameObject enemys;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        player.GetComponent<Player>().numPlayer = numPlayers;
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
           //  Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
           // instancias();
          // activo = true;
            //  Spawn_Manager = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Enemy"));
            //NetworkServer.Spawn(Spawn_Manager);


        }
    }


   // private void Update()
   // {
   //     timeS += 1f*Time.deltaTime;
   //    // Debug.Log(timeS);
   //     timeS2 = Random.RandomRange(2f, 3f);
   //     if (activo)
   //     {
   //         spawn();
   //     }

   //     if (timeS >= timeS2)
   //     {
   //         activo = true;
   //         timeS = 0;
   //     }
   // }
   ////  [Command]
   // void spawn()
   // {
   //     if (activo)
   //     {
   //         Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);

   //         Spawn_Manager2 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Enemy"), posToSpawn, Quaternion.identity);
   //         NetworkServer.Spawn(Spawn_Manager2);

   //         activo = false;
   //     }
   // }
 
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (Spawn_Manager != null)
        {
            NetworkServer.Destroy(Spawn_Manager);
        }

        base.OnServerDisconnect(conn);
    }
}
