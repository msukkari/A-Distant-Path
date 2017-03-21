using System;

public enum AIStates {
	FollowPlayer,
	WaterSearch,
	TurtleState,
	ReturnSpawnWater,
	RandomMovement,
	ReturnSpawn,
	FollowWaypoints,
	Idle,
}

public enum AIEvents {
	PlayerOnNewTile,
	OnMetalCubeRust,
}


public interface AIStateInterface {
		
	// state functions
	void Update();

	// event functions
	void playerOnNewTile();
	void onMetalCubeRust();
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

	public void onMetalCubeRust() {
		Strategy.onMetalCubeRust();
	}
}
