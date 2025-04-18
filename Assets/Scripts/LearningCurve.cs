using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    // переменные класса
    private Transform camTransForm;
    public GameObject directionLight;
    private Transform lightTransform;

    public bool pureOfHeart = true;
    public bool hasSecretIncantation = false;
    public string rareItem = "Staff";
    int currentGold = 12;
    public int number = 1;
    public float secondnumber = 2.3f;
    public string name = "loshara";
    public bool lol = false;
    private int currentAge = 30;
    public int addedAge = 1;
    public bool hasDungeonKey = false;
    public string weaponType = "Arcane Staff";
    public string characterAction = "Attack";

    // Start is called before the first frame update
    void Start()
    {
        //directionLight = GameObject.Find("Directional Light");
        lightTransform = directionLight.GetComponent<Transform>();
        Debug.Log(lightTransform.localPosition);
        /*
        GameObject.Find("Directional Light").GetComponent<Transform>();
        инициализация lightTransform в одной строке
        */



        camTransForm = this.GetComponent<Transform>();
        Debug.Log(camTransForm.localPosition);

        Weapon huntingBow = new Weapon("Hunting Bow", 105);
        Weapon warBow = huntingBow;
        
        warBow.name = "War Bow";
        warBow.damage = 52;
        
        huntingBow.PrintWeaponStats();
        warBow.PrintWeaponStats();
        

        Character heroine = new Character("Agatha");
        heroine.PrintStatsInfo();
        
        Character hero = new Character();
        hero.PrintStatsInfo();

        Character hero2 = hero;
        hero2.name = "Sir Krane the Brave";
        hero.PrintStatsInfo();
        hero2.PrintStatsInfo();

        Paladin knight = new Paladin("Sir Arthur", huntingBow);
        knight.PrintStatsInfo();
        
        int playerNames = 3;

        while(playerNames > 0)
        {
            Debug.Log("Still alive!");
            playerNames--;
        }
        Debug.Log("Player will DIE");

        //создание массива с нулевым значением int[] topPlayerFalls = new int[3];
        //длинный способ создания массива int[] topPlayerScores = new int[] { 713, 549, 984 };
        //короткий способ создания массива
        
        int[] topPlayerScores = { 713, 549, 984 };
        topPlayerScores[1] = 1001;
        Debug.Log(topPlayerScores[1]);

        //Dictionary или Словарь
        int walletCoin = 6;
        Dictionary<string, int> itemInventory = new Dictionary<string, int>()
        {
            {"Potion", 5 },
            {"Antidote", 7 },
            {"Aspirin", 1 }
        };
        
        foreach(KeyValuePair<string, int> kvp in itemInventory)
        {
            if(walletCoin >  kvp.Value)
            {
                Debug.LogFormat("You have something gold:)");
            }
            else
            {
                Debug.LogFormat("Bomzhara POOR");
            }
            Debug.LogFormat("Item: {0} - {1}",kvp.Key, kvp.Value);
        }
          
               /*В следующем примере ключу numberOfPotions будет присвоено значение 5
        int numberOfPotions = itemInventory["Potion"];
        
        itemInventory["Potion"] = 10;
        itemInventory.Add("Throwing Knife", 3);*/

        /*Если для присвоения значения ключу используется обращение по индексу, которого нет в словаре, то компилятор автоматически добавит в словарь новую пару «ключ — значение»*/
        itemInventory["Bandage"] = 5;

        //проверяем, есть ли ключ "Aspirin" в словаре
        if(itemInventory.ContainsKey("Aspirin"))
        {
            itemInventory["Aspirin"] = 3;
        }
        
        //itemInventory.Remove("Antidote");

        Debug.LogFormat("Items: {0}",itemInventory.Count);

        //List или Список - инициализация его и вывод кол-во членов группы в списке методом Count

        List<string> questPartyMembers = new List<string>()
        {"Grim the Barbarian", "Merlin the Wise", "Sterling the Knight" };
        //цикл foreach
        foreach(string partyMembers in questPartyMembers)
        {
            Debug.LogFormat("{0} - Here!", partyMembers);
        }

        //цикл for
        for (int i = 0; i < questPartyMembers.Count; i++)
        {
            Debug.LogFormat("Index: {0} - {1}", i, questPartyMembers[i]);
            if (questPartyMembers[i] == "Merlin the Wise")
            {
                Debug.Log("Glad you're here Merlin!");
            }
        }
        /*questPartyMembers.Add("Craven the Necromancer");
        questPartyMembers.Insert(1, "Tanis the Thief");*/

        /* Оба метода удаляют элемент
        questPartyMembers.RemoveAt(0);
        questPartyMembers.Remove("Grim the Barbarian");*/

        Debug.LogFormat("Party Members: {0}", questPartyMembers.Count);

        // оператор switch
        switch (characterAction)
        {
            case "Heal":
                Debug.Log("Potion sent.");
                break;
            case "Attack":
                Debug.Log("To arms");
                break;
            default:
                Debug.Log("Shields up");
                break;

        }
        //вызов метода в методе Start
        OpenTreasureCamber();
        // операторы if else
        if (!hasDungeonKey)
        {
            Debug.Log("u tebya net klucha");
        }
        else
        {
            Debug.Log("tot kluch");
        }
        if (weaponType != "Long Sword")
        {
            Debug.Log("U tebya ne tot type guna");
        }
        if (currentGold > 50)
        {
            Debug.Log("Zoloto bolsche 50");
        }
        else if (currentGold < 15)
        {
            Debug.Log("Zoloto menshe 15");
        }
        else
        {
            Debug.Log("Ti lox");
        }
        // локальные переменные
        int characterLevel = 32;
        int nextSkillLevel = GenerateCharacter( characterLevel);
        inNumber(GenerateCharacter(characterLevel));
        ComputeAge();
        // вызов окна в консоли
        Debug.Log(nextSkillLevel);
        Debug.Log(GenerateCharacter( characterLevel));
        int diceRoll = 7;
        switch (diceRoll)
        {
            case 7:
            case 15:
                Debug.Log("Mediocre damage, not bad");
                break;
            case 20:
                Debug.Log("Critical hit, thr creature goes down");
                break;
            default:
                Debug.Log("you completely missed and fell onyour face");
                break;
        }

        
        //Debug.Log(name * secondnumber); Не позволяет применить операнду умножения к числу с запятой и слову.
    }
    public int GenerateCharacter(int level)
    {
        Debug.LogFormat("Character:{0} - Level: {1}", name, level);
        return level + 5;
    }
    public void inNumber(int number)
    {
        Debug.Log(number);
    }
    // создание методов
    public void OpenTreasureCamber()
    {
        if (pureOfHeart && rareItem == "Staff")
            
        {
            if (!hasSecretIncantation)
            {
                Debug.Log("you not smart");
            }
            else
            {
                Debug.Log("you the best, take you trasure");
            }
        }
        else
        {
            Debug.Log("come later");
        }
    }
 

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void ComputeAge()

    {
        Debug.Log(currentAge + addedAge);
    }
}   
