using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private Canvas _dialogueCanvas;

    [Header("Food UI")]
    [SerializeField] private GameObject _makeFoodPannel;
    [SerializeField] private GameObject _foodButtonContainer;
    [SerializeField] private Transform _foodPannelOpenPosition;
    [SerializeField] private Transform _foodPannelClosePosition;
    [SerializeField] private float _translationSpeed;
    public GameObject FoodButtonPrefrab;
    [SerializeField] private Vector3 _foodUIPosition;



    [Header("Setup")]
    [SerializeField] private TextDictionaryLine[] _textReferences;

    public static UIManager Instance { get; private set; }

    [Serializable]
    private struct TextDictionaryLine
    {
        public string ElementName;
        public TMPro.TMP_Text TextElementReference;
    }

    private void Awake()
    {
        SingletonCheck();
    }

    private void SingletonCheck()
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
        _foodUIPosition = _foodPannelClosePosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        _makeFoodPannel.transform.position = Vector3.MoveTowards(_makeFoodPannel.transform.position, _foodUIPosition, _translationSpeed * Time.deltaTime);

    }

    public void UpdateText(string uiElementName, string content)
    {
        TMP_Text textElement = FindTextElementByName(uiElementName);

        if (textElement != null)
        {
            textElement.text = content;
        }
    }

    private TMP_Text FindTextElementByName(string elementName)
    {
        foreach (TextDictionaryLine dictionaryLine in _textReferences)
        {
            if (dictionaryLine.ElementName.Equals(elementName))
            {
                return dictionaryLine.TextElementReference;
            }
        }

        Debug.Log("UI Manager: Didn't find a TMPText element with the given name -> " + elementName);

        return null;
    }

    public void PopulateFoodContainer(GameObject[] foodsList)
    {
        Debug.Log("Pupulando");
        foreach (GameObject food in foodsList)
        {
            GameObject newFoodButton = Instantiate(FoodButtonPrefrab, new Vector3(0, 0, 0), Quaternion.identity);
            newFoodButton.transform.SetParent(_foodButtonContainer.transform);
            newFoodButton.SetActive(false);

            AddFoodButton addFoodButton = newFoodButton.GetComponent<AddFoodButton>();

            addFoodButton.ButtonImage.sprite = food.gameObject.GetComponent<SpriteRenderer>().sprite;
            addFoodButton.ButtonText.text = food.GetComponent<FoodController>().name;
            addFoodButton.FoodPrefrab = food;

            newFoodButton.SetActive(true);
        }
        //Instantiate(FoodButtonPrefrab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(_foodButtonContainer.transform);
    }

    public void OpenCloseButton ()
    {
        if (_foodUIPosition == _foodPannelClosePosition.position)
        {
            _foodUIPosition = _foodPannelOpenPosition.position;
        } else
        {
            _foodUIPosition = _foodPannelClosePosition.position;
        }
        
    }

    private void OpenFoodPannel ()
    {

    }

    private void CloseFoodPannel()
    {

    }
}
