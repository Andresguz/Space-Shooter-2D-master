using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Explosion : NetworkBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
}
