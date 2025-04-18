using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomExtensions;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    private int _itemsCollected = 0;

    public Stack<string> lootStack = new Stack<string>();

    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    void Start()
    {
        Initialize();

        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }
    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("GoldenKey");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
        debug(_state);

        LogWithDelegate(debug);
    }
    public static void Print(string newTex)
    {
        Debug.Log(newTex);
    }
    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            UpdateItemLabel();
            CheckWinCondition();
        }
    }

    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            UpdateHealthLabel();
            CheckLossCondition();
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    private void UpdateItemLabel()
    {
        if (_itemsCollected >= maxItems)
        {
            UpdateGameState("You've found all the items!", true, false, 0f);
        }
        else
        {
            labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
        }
    }

    private void UpdateHealthLabel()
    {
        if (_playerHP <= 0)
        {
            UpdateGameState("You want another life with that?", false, true, 0f);
        }
        else
        {
            labelText = "Ouch... that's got hurt";
        }
    }

    private void UpdateGameState(string newLabelText, bool win, bool loss, float timeScale)
    {
        labelText = newLabelText;
        showWinScreen = win;
        showLossScreen = loss;
        Time.timeScale = timeScale;
    }

    private void CheckWinCondition()
    {
        if (_itemsCollected >= maxItems)
        {
            // Дополнительные действия при победе могут быть добавлены здесь
        }
    }

    private void CheckLossCondition()
    {
        if (_playerHP <= 0)
        {
            // Дополнительные действия при поражении могут быть добавлены здесь
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected:" + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if(showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a goof chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }
}
