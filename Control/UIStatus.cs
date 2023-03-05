using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("BaseStatus")]
    public GameObject Player;
    public PlayerStatus playerStatus;
    public Image image;
    public Text _name;
    public Text lv;
    public Text exp;
    public Text hp;
    public Text mp;
    public Text food;
    public string _Name;
    public int level;
    public int currentExperience;
    public int experienceToNextLevel;
    public float maxHp;
    public float currentHp;
    public float maxMp;
    public float maxFood;
    public float currentFood;
    [Header("Status")]
    public Text dmg;
    public Text spd;
    public Text def;
    public Text str;
    public Text inte;
    public Text agi;
    public Text dex;
    public float damage;
    public float attackSpeed;
    public float defense;
    public float strength;
    public float agility;
    public float dexterity;
    public float intelligent;


    private void Start()
    {
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider2d = player.GetComponent<Collider2D>();
                float distance = Vector2.Distance(mouseWorldPosition, collider2d.bounds.center); // คำนวณระยะห่างระหว่างเมาส์และ player
                if (collider2d != null && distance <= collider2d.bounds.extents.magnitude) // เช็คเมื่อเมาส์อยู่ในพื้นที่ของ player
                {
                    if (player != null)
                    {
                        Player = player;
                        playerStatus = Player.GetComponent<PlayerStatus>();
                        image.sprite = playerStatus.monsterSprite;
                    }
                }
            }
        }
        _name.text = _Name;
        lv.text = "lv : " + level;
        exp.text = "Exp :" + currentExperience + "/" + experienceToNextLevel;
        hp.text = "Hp : " + maxHp + "/" + currentHp;
        mp.text = "Mp : " + maxMp + "/" + maxMp;
        food.text = "Food : " + maxFood + "/" + currentFood.ToString();
        dmg.text = "Damage : " + damage.ToString();
        spd.text = "AttackSpeed :" + attackSpeed.ToString();
        def.text = "Defense : " + defense.ToString();
        str.text = "Str : " + strength.ToString();
        agi.text = "Agi : " + agility.ToString();
        dex.text = "Dex : " + dexterity.ToString();
        inte.text = "Int : " + intelligent.ToString();
        if (Player != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _Name = playerStatus.monsterName;
        level = playerStatus.level;
        currentExperience = playerStatus.currentExperience;
        experienceToNextLevel = playerStatus.experienceToNextLevel;
        maxHp = playerStatus.maxHp;
        currentHp = playerStatus.currentHp;
        maxMp = playerStatus.maxMp;
        damage = playerStatus.damage;
        attackSpeed = playerStatus.attackSpeed;
        defense = playerStatus.defense;
        strength = playerStatus.strength;
        agility = playerStatus.agility;
        dexterity = playerStatus.dexterity;
        intelligent = playerStatus.intelligent;
        maxFood = playerStatus.maxFood;
        currentFood = playerStatus.currentFood;
    }
}
