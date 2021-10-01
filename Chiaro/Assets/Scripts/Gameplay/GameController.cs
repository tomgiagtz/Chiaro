using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController> {
    // Start is called before the first frame update
    [SerializeField]
    private int coinCount;
    [SerializeField]
    private int lifeCount = 100;

    public bool isGamePaused;

	public int CoinCount() {
		return this.coinCount;
	}

	//commit for test

	public int LifeCount() {
		return this.lifeCount;
	}

	public void AddCoins(int value) {
        coinCount += value;
    }

    public void RemoveCoins(int value) {
        coinCount -= value;
    }

    public void RemoveLives(int value) {
        lifeCount -= value;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
