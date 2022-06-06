using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private bool clickedFoodBox = false;
    private bool clickedCustomer = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                startSpawning(hit.collider.gameObject.tag);
            }
        }
    }

    private void startSpawning(string objTag)
    {
        switch (objTag)
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

            case "OpenedBox":
                Spawning.instance.closingBox();
                break;

            case "CoffeeMachine":
                Spawning.instance.closingCup();
                break;

            case "Bin":
                Spawning.instance.destroyFoodClone();
                break;

            case "BoxWithFood":
                clickedFoodBox = true;
                break;

            case "Customer":
                if (clickedFoodBox)
                {
                    Spawning.instance.serveFood();
                }
                break;
        }
    }
}
