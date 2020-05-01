using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public RectTransform levelDone;
    public RectTransform levelLocaked;
    public int levelCount;

    private const float buttonOffset = 0.1f; // 10%
    private const int buttonsRow = 5;

    private void Start()
    {
        var rowsCount = Mathf.CeilToInt(levelCount / buttonsRow);
        var xOffset = buttonOffset * levelDone.rect.width;
        var yOffset = buttonOffset * levelDone.rect.height;
        var width = buttonsRow * (levelDone.rect.width + xOffset) - xOffset;
        var height = rowsCount * (levelDone.rect.height + yOffset) - yOffset;
        var startX = -(width / 2f) + levelDone.rect.width / 2f;
        var startY = -(height / 2f) + levelDone.rect.height / 2f;
        var previousLevelFinished = true;

        for (var i = 0; i < levelCount; i++)
        {
            var row = Mathf.FloorToInt(i / buttonsRow);
            var x = startX + (i - row * 5) * (levelDone.rect.width + xOffset);
            var y = startY + (rowsCount - row) * (levelDone.rect.height + yOffset);
            var levelNumber = (i + 1).ToString();
            var levelText = "Level" + levelNumber;

            RectTransform lvl;

            if (StateManager.GetInstance().CheckLevelFinished(levelText))
            {
                lvl = Instantiate(levelDone);
                previousLevelFinished = true;
            }
            else if (previousLevelFinished)
            {
                lvl = Instantiate(levelDone);
                previousLevelFinished = false;
            }
            else
            {
                lvl = Instantiate(levelLocaked);
                previousLevelFinished = false;
            }

            var button = lvl.GetComponent<Button>();
            var text = lvl.Find("LevelNumber").GetComponent<Text>();

            lvl.localPosition = new Vector2(x, y);
            text.text = levelNumber;
            button.onClick.AddListener(() => {
                Helpers.LoadLevel(levelText);
            });

            lvl.transform.SetParent(transform, false);
        }
    }
}
