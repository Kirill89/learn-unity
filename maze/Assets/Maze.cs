using UnityEngine;
using UnityEngine.UI;

public class Maze : MonoBehaviour
{
    public int width = 5;
    public int height = 5;

    public Transform ground;
    public Transform wallV;
    public Transform wallH;
    public Transform roof;

    public RawImage map;

    [System.Flags]
    public enum Wals
    {
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        All = North | East | South | West
    }

    private Wals[,] rooms;

    private void InitPerimeter()
    {
        rooms = new Wals[width, height];

        for (int i = 0; i < width; i++)
        {
            rooms[i, 0] |= Wals.North;
            rooms[i, height - 1] |= Wals.South;
        }

        for (int j = 0; j < height; j++)
        {
            rooms[0, j] |= Wals.West;
            rooms[width - 1, j] |= Wals.East;
        }
    }

    private void RenderWallOnCordinates(bool v, float x, float y)
    {
        var wall = Instantiate(v ? wallV : wallH);

        wall.SetParent(transform, false);
        wall.localPosition = new Vector3(x, wall.localScale.y / 2f, y);
    }

    private void RenderWalls()
    {
        var roomWidth = ground.localScale.x;
        var roomHeight = ground.localScale.z;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var floor = Instantiate(ground);
                var roomCenterX = roomWidth * i;
                var roomCenterY = roomHeight * j;

                floor.SetParent(transform, false);
                floor.localPosition = new Vector3(roomCenterX, 0, roomCenterY);

                var roofTile = Instantiate(roof);
                roofTile.SetParent(transform, false);
                roofTile.localPosition = new Vector3(roomCenterX, wallV.localScale.z, roomCenterY);

                if ((rooms[i, j] & Wals.West) != Wals.None)
                {
                    RenderWallOnCordinates(true, roomCenterX - roomWidth / 2f, roomCenterY);
                }

                if ((rooms[i, j] & Wals.East) != Wals.None)
                {
                    RenderWallOnCordinates(true, roomCenterX + roomWidth / 2f, roomCenterY);
                }

                if ((rooms[i, j] & Wals.North) != Wals.None)
                {
                    RenderWallOnCordinates(false, roomCenterX, roomCenterY - roomHeight / 2f);
                }

                if ((rooms[i, j] & Wals.South) != Wals.None)
                {
                    RenderWallOnCordinates(false, roomCenterX, roomCenterY + roomHeight / 2f);
                }
            }
        }
    }

    private void AddHorizontalWall(int offsetX, int width, int wallPosition)
    {
        var passagePosition = Random.Range(offsetX, offsetX + width);

        for (var i = offsetX; i < offsetX + width; i++)
        {
            if (i != passagePosition)
            {
                rooms[i, wallPosition] |= Wals.South;
                rooms[i, wallPosition + 1] |= Wals.North;
            }
        }
    }

    private void AddVerticalWall(int offsetY, int height, int wallPosition)
    {
        var passagePosition = Random.Range(offsetY, offsetY + height);

        for (var j = offsetY; j < offsetY + height; j++)
        {
            if (j != passagePosition)
            {
                rooms[wallPosition, j] |= Wals.East;
                rooms[wallPosition + 1, j] |= Wals.West;
            }
        }
    }

    private void RecursiveDivision(int offsetX, int offsetY, int width, int height)
    {
        if (width < 2 || height < 2)
        {
            return;
        }

        var isHorizontalCut = Random.Range(0, 2) == 0;
        var wallIdx = isHorizontalCut ? Random.Range(0, height - 1) : Random.Range(0, width - 1);

        if (isHorizontalCut)
        {
            AddHorizontalWall(offsetX, width, offsetY + wallIdx);
            RecursiveDivision(offsetX, offsetY, width, wallIdx + 1);
            RecursiveDivision(offsetX, offsetY + wallIdx + 1, width, height - wallIdx - 1);
        }
        else
        {
            AddVerticalWall(offsetY, height, offsetX + wallIdx);
            RecursiveDivision(offsetX, offsetY, wallIdx + 1, height);
            RecursiveDivision(offsetX + wallIdx + 1, offsetY, width - wallIdx - 1, height);
        }
    }

    private void RenderMap()
    {
        var roomWidth = Mathf.FloorToInt(map.rectTransform.sizeDelta.x / width);
        var roomHeight = Mathf.FloorToInt(map.rectTransform.sizeDelta.y / height);
        var roomHalfWidth = Mathf.FloorToInt(roomWidth / 2f);
        var roomHalfHeight = Mathf.FloorToInt(roomHeight / 2f);

        var texture = new Texture2D(width * roomWidth, height * roomHeight);

        texture.SetPixels(new Color[texture.width * texture.height]);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var roomCenterX = roomWidth * i + roomHalfWidth;
                var roomCenterY = roomHeight * j + roomHalfHeight;

                if ((rooms[i, j] & Wals.West) != Wals.None)
                {
                    for (var p = roomCenterY - roomHalfHeight; p < roomCenterY + roomHalfHeight; p++)
                    {
                        texture.SetPixel(roomCenterX - roomHalfWidth, p, Color.red);
                    }
                }

                if ((rooms[i, j] & Wals.East) != Wals.None)
                {
                    for (var p = roomCenterY - roomHalfHeight; p < roomCenterY + roomHalfHeight; p++)
                    {
                        texture.SetPixel(roomCenterX + roomHalfWidth - 1, p, Color.red);
                    }
                }

                if ((rooms[i, j] & Wals.North) != Wals.None)
                {
                    for (var p = roomCenterX - roomHalfWidth; p < roomCenterX + roomHalfWidth; p++)
                    {
                        texture.SetPixel(p, roomCenterY - roomHalfHeight, Color.red);
                    }
                }

                if ((rooms[i, j] & Wals.South) != Wals.None)
                {
                    for (var p = roomCenterX - roomHalfWidth; p < roomCenterX + roomHalfWidth; p++)
                    {
                        texture.SetPixel(p, roomCenterY + roomHalfHeight - 1, Color.red);
                    }
                }
            }
        }

        texture.Apply();

        map.texture = texture;
    }

    private void Start()
    {
        InitPerimeter();
        RecursiveDivision(0, 0, width, height);
        RenderWalls();
        RenderMap();
    }
}
