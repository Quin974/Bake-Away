using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public static Spawning instance;

    public GameObject box;
    private float boxPosX = 0f;
    private float boxPosY = 0f;
    private GameObject boxClone;
    private bool canSpawnBox = true;

    public GameObject cup;
    private float cupPosX = -6.93f;
    private float cupPosY = -0.01f;
    private GameObject cupClone;
    private bool canSpawnCup = true;

    public GameObject food;
    private float foodPosX = -0.04f;
    private float foodPosY = -0.59f;
    private GameObject foodClone;
    private bool canSpawnFood = true;

    public GameObject closedBox;
    private float closedBoxPosX = 0f;
    private float closedBoxPosY = -0.65f;
    private GameObject closedBoxClone;

    public GameObject closedCup;
    private GameObject closedCupClone;

    private void Awake()
    {
        instance = this;
    }

    public void spawnOpenedBox()
    {
        if (canSpawnBox)
        {
            Vector2 pos = new Vector2(boxPosX, boxPosY);
            boxClone = Instantiate(box, pos, box.transform.rotation);
            canSpawnBox = false;
        }
    }

    public void spawnOpenedCup()
    {
        if (canSpawnCup)
        {
            Vector2 pos = new Vector2(cupPosX, cupPosY);
            cupClone = Instantiate(cup, pos, cup.transform.rotation);
            canSpawnCup = false;
        }
    }

    public void spawnFood()
    {
        if (canSpawnFood && !canSpawnBox)
        {
            Vector2 pos = new Vector2(foodPosX, foodPosY);
            foodClone = Instantiate(food, pos, food.transform.rotation);
            canSpawnFood = false;
        }
    }

    public void closingBox()
    {
        if (!canSpawnBox && !canSpawnFood)
        {
            Vector2 pos = new Vector2(closedBoxPosX, closedBoxPosY);
            Destroy(boxClone);
            Destroy(foodClone);
            closedBoxClone = Instantiate(closedBox, pos, closedBox.transform.rotation);
        }
    }

    public void closingCup()
    {
        if (!canSpawnCup)
        {
            StartCoroutine(waitForDrink());
        }
    }

    IEnumerator waitForDrink()
    {
        Vector2 pos = new Vector2(cupPosX, cupPosY);
        Debug.Log("Here");
        yield return new WaitForSeconds(5.0f);
        Destroy(cupClone);
        closedCupClone = Instantiate(closedCup, pos, closedCup.transform.rotation);
    }

    public void destroyFoodClone()
    {
        Destroy(foodClone);
        canSpawnFood = true;
    }
}
