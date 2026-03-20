using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public enum GameState
{
    Menu,
    Playing,
    GameOver

}

public class GameManager : MonoBehaviour
{
    [Header("Game Panel")]
    public GameObject gamePanel;
    public TextMeshProUGUI gameTimerText;


    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverTimerText;


    [Header("Menu Panel")]
    public GameObject menuPanel;


    [Header("Game Settings")]
    public GameState currentState;
    public Transform tilesParent;
    public GameObject tilePrefab;
    public Tile[] tiles;
    public List<TileUI> tileUiList = new List<TileUI>();



    private float timer;
    private SlotsManager slotsManager;

    void Start()
    {
        slotsManager = GameObject.FindGameObjectWithTag("SlotsManager").GetComponent<SlotsManager>();
        currentState = GameState.Menu;
    }

    void Update()
    {
        if (currentState == GameState.Playing)
        {
            timer += Time.deltaTime;
        }

        gameTimerText.text = UpdateTimerUI(timer);
    }
    public string UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void PlayButton()
    {
        tileUiList.Clear();
        SetTiles();
        ResetTimer();
        currentState = GameState.Playing;
        OpenPanel(gamePanel);
        ClosePanel(menuPanel);
        ClosePanel(gameOverPanel);
        slotsManager.ClearAllSlots();
        timer = 0f;
        Debug.Log("Play button clicked. Starting game...");
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        OpenPanel(gameOverPanel);
        ClosePanel(gamePanel);
        Debug.Log("Game Over!");
        gameOverTimerText.text = UpdateTimerUI(timer);
        foreach (Transform child in tilesParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void MenuButton()
    {
        currentState = GameState.Menu;
        OpenPanel(menuPanel);
        ClosePanel(gameOverPanel);
        Debug.Log("Menu button clicked. Returning to menu...");
    }

    public void SetTiles()
    {
        // Tüm tiles için atama yap
        for (int i = 0; i < tiles.Length; i++)
        {
            // Her tile için 3 adet atama yapacağız
            for (int j = 0; j < 3; j++)
            {
                GameObject tileObj = Instantiate(tilePrefab, tilesParent);
                TileUI tileUI = tileObj.GetComponent<TileUI>();
                tileUI.tile = tiles[i];
                tileUiList.Add(tileUI);
            }
        }


        for (int i = 0; i < tileUiList.Count; i++)
        {
            TileUI temp = tileUiList[i];
            int randomIndex = Random.Range(i, tileUiList.Count);
            tileUiList[i] = tileUiList[randomIndex];
            tileUiList[randomIndex] = temp;
        }
        for (int i = 0; i < tileUiList.Count; i++)
        {
            tileUiList[i].transform.SetSiblingIndex(i);
        }
    }

    public void CheckTiles()
    {
        if (tileUiList.Count == 0)
        {
            GameOver();
            Debug.Log("No more tiles available. Game Over!");
        }
        else
        {
            Debug.Log("Tiles set successfully. Remaining tiles: " + tileUiList.Count);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        DOTween.Sequence()
            .Append(panel.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack))
            .Append(panel.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack));

    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        DOTween.Sequence()
            .Append(panel.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack))
            .Append(panel.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack));
    }
}