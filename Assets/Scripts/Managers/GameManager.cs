using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameState _gameState;
    [SerializeField] private int _playerMoney;

    [Header("Setup")]
    public int MaxNumberOfNPC;

    [Header("References Setup")]
    [SerializeField] private Transform[] _spawnPoints;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupByScene();

    }

    private void SetupByScene()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                SoundManager.Instance.Play("soundtrack1", true, null);
                break;

            case "BaseScene":
                SoundManager.Instance.Play("soundtrack2", true, null);
                SpawnRandomCustomer();

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2[] TransformToVector2(Transform[] transforms)
    {
        Vector2[] vector2Array = new Vector2[transforms.Length];

        for (int i = 0; i < transforms.Length; i++)
        {
            vector2Array[i] = new Vector2(transforms[i].transform.position.x, transforms[i].transform.position.y);
        }
        
        return vector2Array;
    }

    public void SpawnRandomCustomer ()
    {
        NPCManager npcManager = NPCManager.Instance;
        int randomNumber = UnityEngine.Random.Range(0, _spawnPoints.Length-1);

        //GameObject customer = npcManager.CreateCustomer(_spawnPoints[randomNumber].position);
        GameObject customer = npcManager.CreateCustomer(Vector3.zero);

        Vector2[] path = TransformToVector2(_spawnPoints);

        customer.GetComponent<Move>().MoveTo(path);

        //SeatManager.Instance.Seat(npcManager.CustomersWaitingInLine.Dequeue());
    }

    public void DebugVariables()
    {
        Debug.Log("GameManager: ");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public bool SpendMoney (int amount)
    {
        int moneyLeft = _playerMoney - amount;

        if (moneyLeft >= 0)
        {
            _playerMoney = moneyLeft;

            UIManager.Instance.UpdateText("Money", moneyLeft.ToString());

            return true;
        } else
        {
            Debug.LogError("Game Manager: Player doesn't have the required amount to finish the transaction.");

            return false;
        }
    }

    private void Gameplay()
    {
        switch (_gameState)
        {
            case GameState.PLAYING:


                break;
        }
    }

    public void Money(int amount)
    {
        int moneyLeft = _playerMoney - amount;

        _playerMoney = moneyLeft;

        UIManager.Instance.UpdateText("Money", moneyLeft.ToString());
    }
}
