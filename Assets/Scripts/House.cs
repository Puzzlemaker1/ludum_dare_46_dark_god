using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Tile
{
    public int population;
    public int maxPopulation;
    public int popPerHouse;
    public int popRecoverDelay;
    public int maxHousesDisplayable = 4;
    public SpriteRenderer houseSprite;
    public Sprite filledHouse;
    public Sprite ruinedHouse;

    private SpriteRenderer[] houseArray;
    private int curPopRecover;
    private int totalFilledHouses;
    public override void TileStart()
    {
        population = maxPopulation;
        curPopRecover = 0;
        houseArray = new SpriteRenderer[maxHousesDisplayable];
        for(int i = 0; i < maxHousesDisplayable; i++)
        {
            Vector3 position = new Vector3(this.transform.position.x + (i/2 * 0.3f) - 0.2f + Random.Range(-0.01f, 0.01f), 
                this.transform.position.y + (i%2 * 0.4f) - 0.2f + Random.Range(-0.01f, 0.01f));
            houseArray[i] = Instantiate(houseSprite, position, Quaternion.identity, this.transform.parent);
        }
        totalFilledHouses = maxPopulation / popPerHouse;
        if(totalFilledHouses < maxHousesDisplayable)
        {
            int invisibleCount = 0;
            int start = Random.Range(0, maxHousesDisplayable);
            for(int count = (start+1)%maxHousesDisplayable; count != start; count = (count + 1) % maxHousesDisplayable)
            {
                //Start the loop at a random point, so the visible houses are different each time.
                houseArray[count].enabled = false;
                invisibleCount++;
            }
        }
        base.TileStart();
    }
    public override void TileUpdate()
    {
        int housesNeeded = population / popPerHouse;
        if (totalFilledHouses != housesNeeded)
        {
            for (int i = 0; i < houseArray.Length; i++)
            {
                if (houseArray[i].isVisible)
                {
                    if (totalFilledHouses > housesNeeded)
                    {
                        if (houseArray[i].sprite == filledHouse)
                        {
                            houseArray[i].sprite = ruinedHouse;
                            totalFilledHouses--;
                        }
                    }
                    else if(totalFilledHouses < housesNeeded)
                    {
                        if (houseArray[i].sprite == ruinedHouse)
                        {
                            houseArray[i].sprite = filledHouse;
                            totalFilledHouses++;
                        }
                    }
                    else
                    {
                        //We are equal!
                        break;
                    }
                }
            }

        }
        if(population < maxPopulation)
        {
            curPopRecover++;
            if(curPopRecover == popRecoverDelay)
            {
                population++;
                curPopRecover = 0;
            }    
        }
        base.TileUpdate();
    }

    public void reducePop()
    {

    }
}
