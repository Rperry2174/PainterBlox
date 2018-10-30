using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerObject : NetworkBehaviour {

	public GameObject PlayerUnitPrefab;

    // Use this for initialization
    void Start () {
		if( isLocalPlayer == false )
        {
			Debug.Log("PlayerOBject::Start -- isLocalPlayer === false");

			// This belongs to someone else
            return;
        }
        // Since the PlayerObject is invisible give me something physical to move around

        //Debug.Log("PlayerObject::Start -- Spawining my own personal unit.");
		//Instantiate(PlayerUnitPrefab);

		// Command the server to SPAWN out unit
		CmdSpawnMyUnit();
    }

    // Update is called once per frame
	void Update () {
        
	}

    // Commands to be run ONLY on the server
    [Command]
	void CmdSpawnMyUnit()
	{
		Debug.Log("CmdSpawnMyUnit -- Spawining my own personal unit.");

		Vector3 spawnPosition = new Vector3(1.0f, 1.0f, 1.0f);
        
		// We are guarenteed to be on the server now
		GameObject go = Instantiate(PlayerUnitPrefab);

		// Now that the object exists on the server propogate it to all 
		// the clients (and also wire up the NetworkIdentity)
		NetworkServer.Spawn(go);
	}

}
