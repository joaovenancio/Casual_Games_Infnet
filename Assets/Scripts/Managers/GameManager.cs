using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.FilePathAttribute;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameState _gameState;
    [SerializeField] private int _playerMoney;
    [SerializeField] public int _maxNumberOfNPC;
    [SerializeField] private List<FoodListScriptableObject.FoodListData> _foodList;
    private int _activeNPCs;

    [Header("References Setup")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PlayerController _playerController;

    [Header("Data Setup")]
    [SerializeField] private FoodListScriptableObject _foodData;

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

        _foodList = Utils.Convert<FoodListScriptableObject.FoodListData[], List<FoodListScriptableObject.FoodListData>>(_foodData.Foods);

        

        //Debug.Log(_foodList.GetType());
        //Debug.Log(_foodList.Count);
        //Debug.Log(_foodList[0].foodPrefab.GetComponent<FoodController>().Name);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupByScene();
        
        //DialogueManager.Instance.StartDialogue();
    }

    IEnumerator ExampleCoroutine(int time)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        GameObject t = NPCManager.Instance.RemoveFromQueue();
        Debug.Log(t.GetInstanceID());
        SeatManager.Instance.Seat(t);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
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

                InitializeFoodUI();

                GameObject g = SpawnRandomCustomer();
                NPCManager.Instance.AddToQueue(g);

                GameObject go = SpawnRandomCustomer();
                NPCManager.Instance.AddToQueue(go);

                GameObject test = NPCManager.Instance.RemoveFromQueue();
                //Debug.Log(test.GetInstanceID());
                SeatManager.Instance.Seat(test);

                GameObject gosg = SpawnRandomCustomer();
                NPCManager.Instance.AddToQueue(gosg);

                StartCoroutine(ExampleCoroutine(6));


                //NPCManager.Instance.AddToQueue(SpawnRandomCustomer());
                //NPCManager.Instance.AddToQueue(SpawnRandomCustomer());
                //SeatManager.Instance.Seat(NPCManager.Instance.RemoveFromQueue());

                //GameObject gig = SpawnRandomCustomer();
                //NPCManager.Instance.AddToQueue(gig);
                break;

            case "Restaurant 1":
                SoundManager.Instance.Play("soundtrack2", true, null);

                InitializeFoodUI();

                UIManager.Instance.UpdateText("Money", _playerMoney.ToString());

                //GameObject g1 = SpawnRandomCustomer();
                //NPCManager.Instance.AddToQueue(g1);

                //GameObject go1 = SpawnRandomCustomer();
                //NPCManager.Instance.AddToQueue(go1);

                //GameObject test1 = NPCManager.Instance.RemoveFromQueue();

                //SeatManager.Instance.Seat(test1);

                //GameObject gosg1 = SpawnRandomCustomer();
                //NPCManager.Instance.AddToQueue(gosg1);

                //StartCoroutine(ExampleCoroutine(6));


                //NPCManager.Instance.AddToQueue(SpawnRandomCustomer());
                //NPCManager.Instance.AddToQueue(SpawnRandomCustomer());
                //SeatManager.Instance.Seat(NPCManager.Instance.RemoveFromQueue());

                //GameObject gig = SpawnRandomCustomer();
                //NPCManager.Instance.AddToQueue(gig);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gameplay();
    }

    public GameObject SpawnRandomCustomer ()
    {
        NPCManager npcManager = NPCManager.Instance;
        int randomNumber = UnityEngine.Random.Range(0, _spawnPoints.Length);

        return npcManager.SpawnCustomer(_spawnPoints[randomNumber].position);
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
            Debug.LogError("Game Manager: Play" +
                "er doesn't have the required amount to finish the transaction.");

            return false;
        }
    }

    public bool RecieveMoney(int amount)
    {
        _playerMoney = _playerMoney + amount;

        UIManager.Instance.UpdateText("Money", _playerMoney.ToString());

        return true;
    }

    public bool testez = true;

    private void Gameplay()
    {
        switch (_gameState)
        {
            case GameState.PLAYING:

                if (_activeNPCs < _maxNumberOfNPC)
                {
                    GameObject newNPC = SpawnRandomCustomer();
                    _activeNPCs++;
                    NPCManager.Instance.AddToQueue(newNPC);
                }

                //if (testez)
                //{
                //    GameObject test = NPCManager.Instance.RemoveFromQueue();
                //    _activeNPCs--;
                //    SeatManager.Instance.Seat(test);

                //    testez = false;
                //}

                if (SeatManager.Instance.NumberFreeSeats > 0)
                {
                    GameObject test = NPCManager.Instance.RemoveFromQueue();
                    _activeNPCs--;
                    SeatManager.Instance.Seat(test);
                }

                break;
        }
    }

    public GameObject RequestAFood()
    {
        int availableFoods = _foodList.Count;

        //Debug.Log("AAAAAAAAAAAAAAAAA: " + _foodList.Count);

        if (availableFoods >= 1)
        {

            int randomNumber = UnityEngine.Random.Range(0, availableFoods - 1);

            GameObject food = _foodList[randomNumber].foodPrefab;

            return food;
        }
        else
        {
            Debug.Log("Game Manager: There is no available foods on the Food List.");
            return null;
        }
    }

    public void AddFoodToPlayer (GameObject food)
    {
        _playerController.AddFood(food);
    }

    public bool PlayerDeliverFoodTo(NPCControler npc)
    {
        return _playerController.DeliverFood(npc.FoodToOrder.gameObject, npc);
    }

    public void InitializeFoodUI()
    {
        List<GameObject> foodList = new List<GameObject>();

        foreach (FoodListScriptableObject.FoodListData fld in _foodList)
        {
            foodList.Add(fld.foodPrefab);
        }

        UIManager.Instance.PopulateFoodContainer(foodList.ToArray());
    }
    
}
