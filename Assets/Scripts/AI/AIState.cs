using System;

public enum AIStates {
	FollowPlayer,
	WaterSearch,
	ReturnSpawnWater,
	RandomMovement,
	ReturnSpawn,
	FollowWaypoints,
}

public enum AIEvents {
	PlayerOnNewTile,
}


public interface AIStateInterface {
		
	// state functions
	void Update();

	// event functions
	void playerOnNewTile();
}


public class AIState {

	public AIStateInterface Strategy { get; set; }

	public AIState(AIStateInterface strategy) {
		this.Strategy = strategy;
	}

	// -- state functions -- //
	public void Update() {
		Strategy.Update();
	}

	// -- event functions -- //
	public void playerOnNewTile() {
		Strategy.playerOnNewTile();
	}
}
