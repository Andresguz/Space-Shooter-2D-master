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
    public GameObject enemys;
   
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
            Spawn_Manager = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Enemy"));
            NetworkServer.Spawn(Spawn_Manager);

          StartCoroutine(SpawnEnemyRoutine());
        }
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
      //  while (_stopSpawning == false)
        {
            Spawn_Manager2 = Instantiate(enemys, posToSpawn, Quaternion.identity);
            NetworkServer.Spawn(Spawn_Manager2);
            // newEnemy.transform.parent = _enemyContainer.transform;
            Debug.Log("Spawning leaf with leaf count ");
            yield return new WaitForSeconds(5.0f);
        }

    }
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (Spawn_Manager != null)
        {
            NetworkServer.Destroy(Spawn_Manager);
        }

        base.OnServerDisconnect(conn);
    }
}
