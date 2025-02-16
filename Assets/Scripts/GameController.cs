using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class GameController : MonoBehaviour
{
    public List<GameObject> enemies;
    private float spawnTime, currentTime;
    public int shields, health, maxShields, maxHealth;
    public float flakCooldown, flakTimer, laserCooldown, laserTimer, cannonCooldown, cannonTimer;
    public Text enemyTextList;
    public GameObject panelHolder;


    //Screen Parameters
    public int verticalBorder, horizontalBorder, verticalSpacing, horizontalSpacing;
    private Vector3 panelSize; //derived from borders and spacing

    public GameObject blankPanelPrefab, buttonPanelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 4;
        currentTime = spawnTime;
        enemies = new List<GameObject>();
        maxShields = 100;
        maxHealth = 100;
        shields = maxShields;
        health = maxHealth;
        flakCooldown = 4;
        flakTimer = 0;

        panelSize = new Vector3((Screen.width-horizontalBorder)/panelHolder.GetComponent<PanelHolderBehavior>().holderWidth-horizontalSpacing,(Screen.height-verticalBorder)/panelHolder.GetComponent<PanelHolderBehavior>().holderHeight-verticalSpacing);
        Debug.Log(panelSize.ToString());
        PanelHolderBehavior phb = panelHolder.GetComponent<PanelHolderBehavior>();
        phb.panelSize = panelSize;
        phb.verticalBorder = verticalBorder;
        phb.horizontalBorder = horizontalBorder;
        phb.verticalSpacing = verticalSpacing;
        phb.horizontalSpacing = horizontalSpacing;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count < 20)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                SpawnEnemy();
                currentTime = spawnTime;
            }
        }
        //need a timer to ensure shield regen is based on time and not frames

        if (flakTimer > 0)
        {
            flakTimer -= Time.deltaTime;
        }

        //Check key presses during update!
        if (Input.GetKeyUp(KeyCode.Space) && flakTimer <= 0)
        {
            FireFlak();
            flakTimer = flakCooldown;
        }

        //debug behavior for adding panels
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddPanel();
        }


        String textToSet = "";
        foreach (GameObject obj in enemies)
        {
            textToSet += obj.name + " " + obj.GetComponent<Enemy>().distanceFromPlayer + " spacemeters away.\n";
        }

        enemyTextList.text = textToSet;
    }

    void SpawnEnemy()
    {
        GameObject enemy = new GameObject("Generic Enemy");
        enemy.AddComponent<Enemy>();
        enemy.GetComponent<Enemy>().controller = this;
        enemies.Add(enemy);
        Debug.Log("There are " + enemies.Count + " enemies in the list!");
    }

    void FireFlak()
    {
        List<GameObject> toRemove = new List<GameObject>();
        //Destroy all nearby enemies
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().distanceFromPlayer < 100)
            {
                toRemove.Add(enemy);
            }
        }

        foreach (GameObject enemy in toRemove)
        {
            enemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    void FireLaser()
    {
        //this should occur periodically while the laser is on, assuming it has not overheated
    }   
    
    void FireCannon()
    {

    }

    public void AddPanel(GameObject panel = null)
    {
        GameObject toAdd = null;
        if (panel  == null)
        {
            toAdd = Instantiate(blankPanelPrefab);
        } else { toAdd = panel; }

        toAdd.transform.SetParent(panelHolder.transform);

        PanelHolderBehavior ph = panelHolder.GetComponent<PanelHolderBehavior>();
        ph.panelList.Add(toAdd);
    }

    public void DamagePlayer(int damage)
    {
        if (shields >= damage)
        {
            shields -= damage;
        }
        else
        {
            health -= damage - shields;
            shields = 0;
        }
        if (health <=0)
        {
            Debug.Log("UR DED");
        }
    }
}
