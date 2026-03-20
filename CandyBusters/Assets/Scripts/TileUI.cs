using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    private SlotsManager slotsManager;
    private GameManager gameManager;
    public Tile tile;
    [SerializeField] private Image tileImage;

    void Start()
    {
        slotsManager = GameObject.FindGameObjectWithTag("SlotsManager").GetComponent<SlotsManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (tile != null)
        {
            if (tileImage.sprite != tile.tileSprite)
            {
                tileImage.sprite = tile.tileSprite;
            }

        }
    }

    public void OnTileClicked()
    {

        slotsManager.PlaceTileInSlot(tile);
        slotsManager.CheckSlots();
        gameManager.tileUiList.Remove(this);
        gameManager.CheckTiles();
        Destroy(gameObject);

    }
}


