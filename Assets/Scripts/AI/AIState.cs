using System;

public enum AIStates {
	FollowPlayer,		
}

public interface AIStateInterface  {
	void Update();
}


public class AIState {

	public AIStateInterface Strategy { get; set; }

	public AIState(AIStateInterface strategy) {
		this.Strategy = strategy;
	}

	public void Update() {
		Strategy.Update();
	}

}
