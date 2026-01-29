using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int goldReward = 10;
    public int goldPenalty = 10;

    BankManager bank;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bank = FindFirstObjectByType<BankManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardGold()
    {
        if (bank == null) return;
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) return;
        bank.Withdraw(goldPenalty);
    }
}
