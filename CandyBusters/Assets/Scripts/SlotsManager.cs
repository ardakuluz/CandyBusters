using UnityEngine;
using System.Collections.Generic;

public class SlotsManager : MonoBehaviour
{
    public Slot[] slots;
    public Dictionary<TileType, int> sayac = new Dictionary<TileType, int>();
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    public void PlaceTileInSlot(Tile tile)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].PlaceTile(tile);
                break;
            }
        }

    }

    public void CheckSlots()
    {
        sayac.Clear(); // önce temizle
        foreach (var slot in slots)
        {
            if (slot.currentTile == null)
                continue;
            var data = slot.currentTile.tileType;
            if (sayac.ContainsKey(data))
                sayac[data]++;
            else
                sayac[data] = 1;
        }

        foreach (var item in sayac)
        {
            if (item.Value == 3)
            {
                List<Slot> slotsToClear = new List<Slot>();
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].currentTile != null && slots[i].currentTile.tileType == item.Key)
                    {
                        //Debug.Log("Clearing slot " + i + " with tile type " + item.Key);
                        slotsToClear.Add(slots[i]);
                    }
                }

                for (int i = 0; i < slotsToClear.Count; i++)
                {
                    slotsToClear[i].ClearSlot();
                }
            }
        }

        if (isAllSlotsFilled())
        {
            gameManager.ResetTimer();
            gameManager.GameOver();
        }
    }



    public void ResortSlots()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].IsEmpty() && !slots[i + 1].IsEmpty())
            {
                slots[i].PlaceTile(slots[i + 1].currentTile);
                slots[i + 1].ClearSlot();
            }
        }
    }

    public bool isAllSlotsFilled()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
                return false;
        }
        return true;
    }

    public void ClearAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Slot>().ClearSlot();
        }
    }
}
