using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject phaseIndicator;
    public bool landPerTurnPlayer = false;
    public bool landPerTurnEnemy = false;
    public GameObject playerLandArea;
    public GameObject enemyLandArea;
    public string phase = null;

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

    bool isGameRunning = false;

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

        isGameRunning = true;

        if(coinFlip == 0)
        {
            phase = "Main1";
            SetTickColor(main1, 1, 0, 0, 1);
        }
        else
        {
            phase = "Main1 Opp";
            SetTickColor(main1Opp, 1, 0, 0, 1);
        }

    }

    public void NextPhase()
    {
        switch(phase)
        {
            case "Untap":
                phase = "Upkeep";
                landPerTurnPlayer = false;
                SetTickColor(upkeep, 1, 0, 0, 1);
                SetTickColor(untap, 1, 1, 1, 1);
                break;

            case "Untap Opp":
                phase = "Upkeep Opp";
                landPerTurnEnemy = false;
                SetTickColor(upkeepOpp, 1, 0, 0, 1);
                SetTickColor(untapOpp, 1, 1, 1, 1);
                break;

            case "Upkeep":
                phase = "Draw";
                SetTickColor(draw, 1, 0, 0, 1);
                SetTickColor(upkeep, 1, 1, 1, 1);
                break;

            case "Upkeep Opp":
                phase = "Draw Opp";
                SetTickColor(drawOpp, 1, 0, 0, 1);
                SetTickColor(upkeepOpp, 1, 1, 1, 1);
                break;

            case "Draw":
                phase = "Main1";
                SetTickColor(main1, 1, 0, 0, 1);
                SetTickColor(draw, 1, 1, 1, 1);
                break;

            case "Draw Opp":
                phase = "Main1 Opp";
                SetTickColor(main1Opp, 1, 0, 0, 1);
                SetTickColor(drawOpp, 1, 1, 1, 1);
                break;

            case "Main1":
                phase = "Begin Combat";
                SetTickColor(beginCombat, 1, 0, 0, 1);
                SetTickColor(main1, 1, 1, 1, 1);
                break;

            case "Main1 Opp":
                phase = "Begin Combat Opp";
                SetTickColor(beginCombatOpp, 1, 0, 0, 1);
                SetTickColor(main1Opp, 1, 1, 1, 1);
                break;

            case "Begin Combat":
                phase = "Attackers";
                SetTickColor(attackers, 1, 0, 0, 1);
                SetTickColor(beginCombat, 1, 1, 1, 1);
                break;

            case "Begin Combat Opp":
                phase = "Attackers Opp";
                SetTickColor(attackersOpp, 1, 0, 0, 1);
                SetTickColor(beginCombatOpp, 1, 1, 1, 1);
                break;

            case "Attackers":
                phase = "Blockers";
                SetTickColor(blockers, 1, 0, 0, 1);
                SetTickColor(attackers, 1, 1, 1, 1);
                break;

            case "Attackers Opp":
                phase = "Blockers Opp";
                SetTickColor(blockersOpp, 1, 0, 0, 1);
                SetTickColor(attackersOpp, 1, 1, 1, 1);
                break;

            case "Blockers":
                phase = "Damage";
                SetTickColor(damage, 1, 0, 0, 1);
                SetTickColor(blockers, 1, 1, 1, 1);
                break;

            case "Blockers Opp":
                phase = "Damage Opp";
                SetTickColor(damageOpp, 1, 0, 0, 1);
                SetTickColor(blockersOpp, 1, 1, 1, 1);
                break;

            case "Damage":
                phase = "Main2";
                SetTickColor(main2, 1, 0, 0, 1);
                SetTickColor(damage, 1, 1, 1, 1);
                break;

            case "Damage Opp":
                phase = "Main2 Opp";
                SetTickColor(main2Opp, 1, 0, 0, 1);
                SetTickColor(damageOpp, 1, 1, 1, 1);
                break;

            case "Main2":
                phase = "End";
                SetTickColor(end, 1, 0, 0, 1);
                SetTickColor(main2, 1, 1, 1, 1);
                break;

            case "Main2 Opp":
                phase = "End Opp";
                SetTickColor(endOpp, 1, 0, 0, 1);
                SetTickColor(main2Opp, 1, 1, 1, 1);
                break;

            case "End":
                phase = "Untap Opp";
                SetTickColor(untapOpp, 1, 0, 0, 1);
                SetTickColor(end, 1, 1, 1, 1);
                UntapEnemyLands();
                break;

            case "End Opp":
                phase = "Untap";
                SetTickColor(untap, 1, 0, 0, 1);
                SetTickColor(endOpp, 1, 1, 1, 1);
                UntapPlayerLands();
                break;
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
}
