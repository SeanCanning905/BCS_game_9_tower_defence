using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankManager : MonoBehaviour
{
    [SerializeField] int startingBalance = 100;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI balanceText;

    public int CurrentBalance
    {
        get
        {
            return currentBalance; 
        }
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalance();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalance();

        if (currentBalance < 0)
        {
            // add game over sequence
            ReloadScene();
        }
    }

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateBalance();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateBalance()
    {
        balanceText.text = currentBalance.ToString();
    }
}
