using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject manaPool;
    public GameObject phaseIndicator;
    public bool landPerTurnPlayer = false;
    public bool landPerTurnEnemy = false;
    public GameObject playerLandArea;
    public GameObject enemyLandArea;
    public GameObject playerBattlefield;
    public GameObject enemyBattlefield;
    public GameObject theStack;
    public string phase = null;
    public string priority = null;
    public int timesPassed = 0;

    public int playerManaRed = 0;
    public int playerManaBlue = 0;
    public int playerManaGreen = 0;
    public int playerManaBlack = 0;
    public int playerManaWhite = 0;
    public int playerManaColorless = 0;

    public int enemyManaRed = 0;
    public int enemyManaBlue = 0;
    public int enemyManaGreen = 0;
    public int enemyManaBlack = 0;
    public int enemyManaWhite = 0;
    public int enemyManaColorless = 0;

    GameObject untap;
    GameObject upkeep;
    GameObject draw;
    GameObject main1;
    GameObject beginCombat;
    GameObject attackers;
    GameObject blockers;
    GameObject damage;
    GameObject main2;
    GameObject end;
    GameObject untapOpp;
    GameObject upkeepOpp;
    GameObject drawOpp;
    GameObject main1Opp;
    GameObject beginCombatOpp;
    GameObject attackersOpp;
    GameObject blockersOpp;
    GameObject damageOpp;
    GameObject main2Opp;
    GameObject endOpp;

    


    // Start is called before the first frame update
    void Start()
    {
        untap = phaseIndicator.transform.GetChild(0).gameObject;
        upkeep = phaseIndicator.transform.GetChild(1).gameObject;
        draw = phaseIndicator.transform.GetChild(2).gameObject;
        main1 = phaseIndicator.transform.GetChild(3).gameObject;
        beginCombat = phaseIndicator.transform.GetChild(4).gameObject;
        attackers = phaseIndicator.transform.GetChild(5).gameObject;
        blockers = phaseIndicator.transform.GetChild(6).gameObject;
        damage = phaseIndicator.transform.GetChild(7).gameObject;
        main2 = phaseIndicator.transform.GetChild(8).gameObject;
        end = phaseIndicator.transform.GetChild(9).gameObject;
        untapOpp = phaseIndicator.transform.GetChild(10).gameObject;
        upkeepOpp = phaseIndicator.transform.GetChild(11).gameObject;
        drawOpp = phaseIndicator.transform.GetChild(12).gameObject;
        main1Opp = phaseIndicator.transform.GetChild(13).gameObject;
        beginCombatOpp = phaseIndicator.transform.GetChild(14).gameObject;
        attackersOpp = phaseIndicator.transform.GetChild(15).gameObject;
        blockersOpp = phaseIndicator.transform.GetChild(16).gameObject;
        damageOpp = phaseIndicator.transform.GetChild(17).gameObject;
        main2Opp = phaseIndicator.transform.GetChild(18).gameObject;
        endOpp = phaseIndicator.transform.GetChild(19).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set isGameRunning to true and randomly select which player goes first.
    public void StartGame()
    {
        int coinFlip = Random.Range(0, 2);

        if(coinFlip == 0)
        {
            phase = "Main1";
            SetPriority("Player");
            timesPassed = 0;
            SetTickColor(main1, 1, 0, 0, 1);
        }
        else
        {
            phase = "Main1 Opp";
            SetPriority("Enemy");
            timesPassed = 0;
            SetTickColor(main1Opp, 1, 0, 0, 1);
        }

    }

    public void NextPhase()
    {
        timesPassed++;

        if(priority == "Player")
        {
            SetPriority("Enemy");
        }
        else if(priority == "Enemy")
        {
            SetPriority("Player");
        }

        if((timesPassed >= 2 && theStack.transform.childCount == 0) || priority == "Nobody")
        {
            timesPassed = 0;

            switch (phase)
            {
                case "Untap":
                    phase = "Upkeep";
                    landPerTurnPlayer = false;
                    SetTickColor(upkeep, 1, 0, 0, 1);
                    SetTickColor(untap, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Untap Opp":
                    phase = "Upkeep Opp";
                    landPerTurnEnemy = false;
                    SetTickColor(upkeepOpp, 1, 0, 0, 1);
                    SetTickColor(untapOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Upkeep":
                    phase = "Draw";
                    SetTickColor(draw, 1, 0, 0, 1);
                    SetTickColor(upkeep, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Upkeep Opp":
                    phase = "Draw Opp";
                    SetTickColor(drawOpp, 1, 0, 0, 1);
                    SetTickColor(upkeepOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Draw":
                    phase = "Main1";
                    SetTickColor(main1, 1, 0, 0, 1);
                    SetTickColor(draw, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Draw Opp":
                    phase = "Main1 Opp";
                    SetTickColor(main1Opp, 1, 0, 0, 1);
                    SetTickColor(drawOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Main1":
                    phase = "Begin Combat";
                    SetTickColor(beginCombat, 1, 0, 0, 1);
                    SetTickColor(main1, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Main1 Opp":
                    phase = "Begin Combat Opp";
                    SetTickColor(beginCombatOpp, 1, 0, 0, 1);
                    SetTickColor(main1Opp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Begin Combat":
                    phase = "Attackers";
                    SetTickColor(attackers, 1, 0, 0, 1);
                    SetTickColor(beginCombat, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Begin Combat Opp":
                    phase = "Attackers Opp";
                    SetTickColor(attackersOpp, 1, 0, 0, 1);
                    SetTickColor(beginCombatOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Attackers":
                    phase = "Blockers";
                    SetTickColor(blockers, 1, 0, 0, 1);
                    SetTickColor(attackers, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Attackers Opp":
                    phase = "Blockers Opp";
                    SetTickColor(blockersOpp, 1, 0, 0, 1);
                    SetTickColor(attackersOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Blockers":
                    phase = "Damage";
                    SetTickColor(damage, 1, 0, 0, 1);
                    SetTickColor(blockers, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Blockers Opp":
                    phase = "Damage Opp";
                    SetTickColor(damageOpp, 1, 0, 0, 1);
                    SetTickColor(blockersOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Damage":
                    phase = "Main2";
                    SetTickColor(main2, 1, 0, 0, 1);
                    SetTickColor(damage, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Damage Opp":
                    phase = "Main2 Opp";
                    SetTickColor(main2Opp, 1, 0, 0, 1);
                    SetTickColor(damageOpp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "Main2":
                    phase = "End";
                    SetTickColor(end, 1, 0, 0, 1);
                    SetTickColor(main2, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Player");
                    break;

                case "Main2 Opp":
                    phase = "End Opp";
                    SetTickColor(endOpp, 1, 0, 0, 1);
                    SetTickColor(main2Opp, 1, 1, 1, 1);
                    EmptyManaPools();
                    SetPriority("Enemy");
                    break;

                case "End":
                    phase = "Untap Opp";
                    SetTickColor(untapOpp, 1, 0, 0, 1);
                    SetTickColor(end, 1, 1, 1, 1);
                    UntapEnemyLands();
                    UntapEnemyBattlefield();
                    EmptyManaPools();
                    SetPriority("Nobody");
                    break;

                case "End Opp":
                    phase = "Untap";
                    SetTickColor(untap, 1, 0, 0, 1);
                    SetTickColor(endOpp, 1, 1, 1, 1);
                    UntapPlayerLands();
                    UntapPlayerBattlefield();
                    EmptyManaPools();
                    SetPriority("Nobody");
                    break;
            }
        }
        else if (timesPassed >= 2)
        {
            timesPassed = 0;

            theStack.GetComponent<TheStack>().ResolveStack();
        }
        
    }

    //Set the color of the tickBox passed to the rgba value passed.
    void SetTickColor(GameObject tickBox, float r, float g, float b, float a)
    {
        ColorBlock color = tickBox.GetComponent<Toggle>().colors;
        color.normalColor = new Color(r, g, b, a);
        tickBox.GetComponent<Toggle>().colors = color;
    }

    //Untap all player lands.
    void UntapPlayerLands()
    {
        foreach(Transform child in playerLandArea.transform)
        {
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //Untap all enemy lands.
    void UntapEnemyLands()
    {
        foreach (Transform child in enemyLandArea.transform)
        {
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void UntapPlayerBattlefield()
    {
        foreach (Transform child in playerBattlefield.transform)
        {
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void UntapEnemyBattlefield()
    {
        foreach (Transform child in enemyBattlefield.transform)
        {
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //Set all mana pools to zero and update mana pool UI.
    void EmptyManaPools()
    {
        playerManaRed = 0;
        manaPool.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Red: " + playerManaRed.ToString();
        playerManaBlue = 0;
        manaPool.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Blue: " + playerManaBlue.ToString();
        playerManaGreen = 0;
        manaPool.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Green: " + playerManaGreen.ToString();
        playerManaBlack = 0;
        manaPool.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Black: " + playerManaBlack.ToString();
        playerManaWhite = 0;
        manaPool.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "White: " + playerManaWhite.ToString();
        playerManaColorless = 0;
        manaPool.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Colorless: " + playerManaColorless.ToString();

        enemyManaRed = 0;
        enemyManaBlue = 0;
        enemyManaGreen = 0;
        enemyManaBlack = 0;
        enemyManaWhite = 0;
        enemyManaColorless = 0;
    }

    void SetPriority(string player)
    {
        priority = player;
        phaseIndicator.transform.GetChild(20).GetComponent<TextMeshProUGUI>().text = "Priority: " + player;

    }

    //Add one to the correct players mana pool depending on the color and player input. Then update mana pool UI.
    public void addToManaPool(char color, string player, int amount)
    {
        if(player == "Player")
        {
            switch(color)
            {
                case 'r':
                    playerManaRed += amount;
                    manaPool.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Red: " + playerManaRed.ToString();
                    break;

                case 'u':
                    playerManaBlue += amount;
                    manaPool.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Blue: " + playerManaBlue.ToString();
                    break;

                case 'g':
                    playerManaGreen += amount;
                    manaPool.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Green: " + playerManaGreen.ToString();
                    break;

                case 'b':
                    playerManaBlack += amount;
                    manaPool.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Black: " + playerManaBlack.ToString();
                    break;

                case 'w':
                    playerManaWhite += amount;
                    manaPool.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "White: " + playerManaWhite.ToString();
                    break;

                case 'c':
                    playerManaColorless += amount;
                    manaPool.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Colorless: " + playerManaColorless.ToString();
                    break;
            }
        
        }
        else if(player == "Enemy")
        {
            switch (color)
            {
                case 'r':
                    enemyManaRed += amount;
                    Debug.Log("Red: " + enemyManaRed.ToString());
                    break;

                case 'u':
                    enemyManaBlue += amount;
                    Debug.Log("Blue: " + enemyManaBlue.ToString());
                    break;

                case 'g':
                    enemyManaGreen += amount;
                    Debug.Log("Green: " + enemyManaGreen.ToString());
                    break;

                case 'b':
                    enemyManaBlack += amount;
                    Debug.Log("Black: " + enemyManaBlack.ToString());
                    break;

                case 'w':
                    enemyManaWhite += amount;
                    Debug.Log("White: " + enemyManaWhite.ToString());
                    break;

                case 'c':
                    enemyManaColorless += amount;
                    Debug.Log("Colorless: " + enemyManaColorless.ToString());
                    break;
            }
        }
            


        
    }
}
