using UnityEngine;
using UnityEngine.UI;

enum SlotState
{
    Empty,
    Occupied
}

public class Slot : MonoBehaviour
{
    public Tile currentTile;
    private SlotState slotState;
    [SerializeField] private Image tileImage;

    void Start()
    {
        slotState = SlotState.Empty;
    }

    public void PlaceTile(Tile tile)
    {
        currentTile = tile;
        slotState = SlotState.Occupied;
        tileImage.sprite = tile.tileSprite; // Update yerine buraya koy
    }

    public void ClearSlot()
    {
        currentTile = null;
        slotState = SlotState.Empty;
        tileImage.sprite = null; // Update yerine buraya koy
    }
    public bool IsEmpty()
    {
        return slotState == SlotState.Empty;
    }
}
