using UnityEngine;

public enum TileType
{
    Apple,
    Banana,
    Cherry,
    Grape,
    Orange,
    Strawberry,
    Watermelon,
    Peach
}

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Tile")]
public class Tile : ScriptableObject
{
    public TileType tileType;
    public Sprite tileSprite;
}
