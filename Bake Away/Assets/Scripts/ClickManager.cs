using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private bool clickedFoodBox = false;
    private bool clickedCoffeeCup = false;
    private bool canClick = true;
    private string drinkName = " ";
    public Sprite[] highlight;
    private Sprite tmpSprite;
    private GameObject tmpObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                startSpawning(hit.collider.gameObject);
            }
        }
    }

    private void startSpawning(GameObject obj)
    {
        switch (obj.tag)
        {
            case "ClosedBox":
                Spawning.instance.spawnOpenedBox();
                break;

            case "ClosedCup":
                Spawning.instance.spawnOpenedCup();
                break;

            case "CheeseCake":
                Spawning.instance.spawnFood(0);
                break;

            case "Tiramisu":
                Spawning.instance.spawnFood(1);
                break;

            case "Donut":
                Spawning.instance.spawnFood(2);
                break;

            case "Cupcake":
                Spawning.instance.spawnFood(3);
                break;

            case "OpenedBox":
                Spawning.instance.closingBox();
                break;

            case "CoffeeMachine":
                Spawning.instance.closingCup();
                break;

            case "Bin":
                if (clickedFoodBox)
                {
                    Spawning.instance.destroyFoodClone();
                    clickedFoodBox = false;
                    canClick = true;
                }

                else
                {
                    Spawning.instance.destroyFoodClone();
                }

                break;

            case "BoxWithFood":
                if (canClick && !clickedFoodBox)
                {
                    clickedFoodBox = true;
                    highLight(obj, 0);
                    canClick = false;
                }

                else if (!canClick && clickedFoodBox)
                {
                    clickedFoodBox = false;
                    deHighLight(tmpObj);
                    canClick = true;
                }

                break;

            case "CupWithCoffee":
                if (canClick && !clickedCoffeeCup)
                {
                    clickedCoffeeCup = true;
                    drinkName = obj.GetComponent<SpriteRenderer>().sprite.name;
                    highLight(obj, 1);
                    canClick = false;
                }

                else if (!canClick && clickedCoffeeCup)
                {
                    clickedCoffeeCup = false;
                    drinkName = " ";
                    deHighLight(tmpObj);
                    canClick = true;
                }
                break;

            case "Customer":
                if (clickedFoodBox)
                {
                    if (Spawning.instance.checkOrder(obj))
                    {
                        Spawning.instance.serveFood();
                        obj.GetComponent<CustomerTimer>().stopClock();
                        Spawning.instance.happyCustomerLeaving(obj);
                        clickedFoodBox = false;
                        canClick = true;
                    }

                    else
                    {
                        obj.GetComponent<CustomerTimer>().newTry();
                        obj.GetComponent<CustomerTimer>().chancesCheck(obj);
                        deHighLight(tmpObj);
                        
                        if (obj != null)
                        {
                            Spawning.instance.angryCustomer(obj);
                        }

                        clickedFoodBox = false;
                        canClick = true;
                    }  
                }

                else if (clickedCoffeeCup)
                {
                    if (Spawning.instance.checkDrink(obj, drinkName))
                    {
                        Spawning.instance.serveDrink();
                        obj.GetComponent<CustomerTimer>().stopClock();
                        Spawning.instance.happyCustomerLeaving(obj);
                        clickedCoffeeCup = false;
                        canClick = true;
                    }

                    else
                    {
                        obj.GetComponent<CustomerTimer>().newTry();
                        obj.GetComponent<CustomerTimer>().chancesCheck(obj);
                        deHighLight(tmpObj);
                        
                        if (obj != null)
                        {
                            Spawning.instance.angryCustomer(obj);
                        }

                        clickedCoffeeCup = false;
                        canClick = true;
                    }
                }

                break;
        }
    }

    private void highLight(GameObject obj, int i)
    {
        tmpObj = obj;
        tmpSprite = obj.GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<SpriteRenderer>().sprite = highlight[i];
    }

    private void deHighLight(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().sprite = tmpSprite;
    }
}
