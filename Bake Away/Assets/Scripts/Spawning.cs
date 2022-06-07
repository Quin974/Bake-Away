using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public static Spawning instance;

    public GameObject box;
    private float boxPosX = 0f;
    private float boxPosY = -0.42f;
    private GameObject boxClone;
    private bool canSpawnBox = true;

    public GameObject cup;
    private float cupPosX = -6.93f;
    private float cupPosY = -0.01f;
    private GameObject cupClone;
    private bool canSpawnCup = true;

    public Sprite[] foodSprite;
    private SpriteRenderer foodRenderer;
    private bool canSpawnFood = true;

    public GameObject closedBox;
    private float closedBoxPosX = 0f;
    private float closedBoxPosY = -0.91f;
    private GameObject closedBoxClone;
    private SpriteRenderer foodChosenRenderer;

    public GameObject closedCup;
    private GameObject closedCupClone;

    public GameObject customer;
    private int i = -1;
    private List<Vector2> customerPosList;
    private List<Vector2> existPosList;
    private List<GameObject> customerClone;
    private bool canLeave = false;

    private float maxTime = 10.0f;
    private float minTime = 3.0f;
    private float time;
    private float spawnTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        customerPosList = new List<Vector2>();
        existPosList = new List<Vector2>();
        customerClone = new List<GameObject>();

        Vector2 pos1 = new Vector2(-3.29f, -0.19f);
        Vector2 pos2 = new Vector2(0, -0.19f);
        Vector2 pos3 = new Vector2(3.51f, -0.19f);

        customerPosList.Add(pos1);
        customerPosList.Add(pos2);
        customerPosList.Add(pos3);

        setRandomTime();
        time = 0;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= spawnTime && existPosList.Count < 3)
        {
            spawnCustomer();
            setRandomTime();
            time = 0;
        }
    }

    public void spawnOpenedBox()
    {
        if (canSpawnBox)
        {
            Vector2 pos = new Vector2(boxPosX, boxPosY);
            boxClone = Instantiate(box, pos, box.transform.rotation);
            foodRenderer = boxClone.transform.Find("Food").GetComponent<SpriteRenderer>();
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

    public void spawnFood(int index)
    {
        if (canSpawnFood && !canSpawnBox)
        {
            foodRenderer.sprite = foodSprite[index];
            canSpawnFood = false;
        }
    }

    public void closingBox()
    {
        if (!canSpawnBox && !canSpawnFood)
        {
            int index = 0;
            Vector2 pos = new Vector2(closedBoxPosX, closedBoxPosY);
            string foodName = foodRenderer.sprite.name;
            Destroy(boxClone);

            for (int i = 0; i < foodSprite.Length; i++)
            {
                if (foodSprite[i].name == foodName)
                {
                    index = i;
                }
            }

            closedBoxClone = Instantiate(closedBox, pos, closedBox.transform.rotation);
            foodChosenRenderer = closedBoxClone.transform.Find("Food").GetComponent<SpriteRenderer>();
            foodChosenRenderer.sprite = foodSprite[index];
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
        yield return new WaitForSeconds(5.0f);
        Destroy(cupClone);
        closedCupClone = Instantiate(closedCup, pos, closedCup.transform.rotation);
    }

    public void destroyFoodClone()
    {
        if (foodRenderer != null)
        {
            foodRenderer.sprite = null;
            canSpawnFood = true;
        }

        else
        {
            Destroy(closedBoxClone);
            canSpawnFood = true;
            canSpawnBox = true;
        }
    }

    private void setRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    private void spawnCustomer()
    {
        int posIndex = 0;
        i++;

        if (existPosList.Count < 3)
        {
            do
            {
                posIndex = Random.Range(0, customerPosList.Count);
            } while (checkOverlap(customerPosList[posIndex]));
            GameObject tmp = Instantiate(customer, customerPosList[posIndex], customer.transform.rotation);
            customerClone.Add(tmp);
            existPosList.Add(customerPosList[posIndex]);
        }
    }

    private bool checkOverlap(Vector2 pos)
    {
        bool check = false;

        foreach (Vector2 item in existPosList)
        {
            if (pos == item)
            {
                check = true;
                break;
            }
        }
        return check;
    }

    public bool checkOrder(GameObject obj)
    {
        bool check = false;
        string foodName = foodChosenRenderer.sprite.name;
        string foodOrder = obj.transform.Find("Order").GetComponent<SpriteRenderer>().sprite.name;

        if (foodName == foodOrder)
        {
            check = true;
        }

        return check;
    }

    public bool checkDrink(GameObject obj, string drinkName)
    {
        bool check = false;
        string drinkOrder = obj.transform.Find("Order").GetComponent<SpriteRenderer>().sprite.name;
        if (drinkOrder == drinkName)
        {
            check = true;
        }

        return check;
    }

    public void serveFood()
    {
        Destroy(closedBoxClone);
        canSpawnBox = true;
        canSpawnFood = true;
        canLeave = true;
    }

    public void serveDrink()
    {
        Destroy(closedCupClone);
        canSpawnCup = true;
        canLeave = true;
    }

    public void customerLeave(GameObject obj)
    {
        for (int i = 0; i < customerClone.Count; i++)
        {
            if (obj.transform.position == customerClone[i].transform.position && canLeave)
            {
                Destroy(customerClone[i].gameObject);
                customerClone.RemoveAt(i);
                canLeave = false;
            }
        }
    }
}
