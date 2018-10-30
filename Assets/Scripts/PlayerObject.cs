using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject PlayerUnitPrefab;

	// Use this for initialization
	void Start () {

		if (isLocalPlayer == false)
		{
			return;
		}
		// Since the playerOBject is invisibile and not part of the world,
		// give me something physical to move around!
		//Debug.Log("PlayerOBject::start -- Spawning my own personal unit.");
		// Instantiate(PlayerUnitPrefab, new Vector3(2.0f, 2.0f, 2.0f), gameObject.transform.rotation);
		CmdSpawnMyUnit();
	}

	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	void CmdSpawnMyUnit() {
		Debug.Log("CmdSpawnMyUnit() -- Spawning my own personal unit.");

		GameObject go = Instantiate(PlayerUnitPrefab, new Vector3(2.0f, 2.0f, 2.0f), gameObject.transform.rotation);

		// Now that the object exists on the server, propagate it to all
		// the clients (and also wire up the NetworkIdentity)

		NetworkServer.Spawn(go);

	}
}
