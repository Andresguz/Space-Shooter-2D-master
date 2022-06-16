using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Spawn : NetworkBehaviour
{
    public GameObject enemy;
    private float timeS;
    private float timeS2;
    bool activo=false;
   [Command]
   public void CmdSpwan()
    {
        if (activo)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);

            GameObject enemyt = Instantiate(enemy, posToSpawn, Quaternion.identity);
            NetworkServer.Spawn(enemyt);
            activo =false;
        }
  
      
    }
    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            timeS += 1.5f * Time.deltaTime;
            // Debug.Log(timeS);
            timeS2 = Random.RandomRange(2f, 5f);
            if (activo)
            {
                CmdSpwan();
            }

            if (timeS >= timeS2)
            {
                activo = true;
                timeS = 0;
            }
        }
    }
}
