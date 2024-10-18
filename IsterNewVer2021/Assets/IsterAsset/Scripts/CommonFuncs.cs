using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFuncs
{
    #region CalcDir
    public static Vector3 CalcDir(Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from).normalized;
        return dir;
    }

    public static Vector3 CalcDir(Vector3 from, Component to)
    {
        Vector3 dir = (to.transform.position - from).normalized;
        return dir;
    }

    public static Vector3 CalcDir(Component from, Vector3 to)
    {
        Vector3 dir = (to - from.transform.position).normalized;
        return dir;
    }

    public static Vector3 CalcDir(Component from, Component to)
    {
        Vector3 dir = (to.transform.position - from.transform.position).normalized;
        return dir;
    }
    #endregion

    #region DirToRadian
    public static float DirToRadian(Vector2 dir)
    {
        float dot = Vector3.Dot(Vector2.right, dir.normalized);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        return angle;
    }

    public static float DirToRadian(Vector3 from, Vector3 to)
    {
        Vector2 dir = CalcDir(from, to);
        float dot = Vector3.Dot(Vector2.right, dir.normalized);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        return angle;
    }

    public static float DirToRadian(Component from, Vector3 to)
    {
        Vector2 dir = CalcDir(from, to);
        float dot = Vector3.Dot(Vector2.right, dir.normalized);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        return angle;
    }

    public static float DirToRadian(Vector3 from, Component to)
    {
        Vector2 dir = CalcDir(from, to);
        float dot = Vector3.Dot(Vector2.right, dir.normalized);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        return angle;
    }

    public static float DirToRadian(Component from, Component to)
    {
        Vector2 dir = CalcDir(from, to);
        float dot = Vector3.Dot(Vector2.right, dir.normalized);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        return angle;
    }
    #endregion

    #region DirToDegree
    public static float DirToDegree(Vector2 dir)
    {
        return DirToRadian(dir) * Mathf.Rad2Deg;
    }

    public static float DirToDegree(Vector3 from, Vector3 to)
    {
        return DirToRadian(from, to) * Mathf.Rad2Deg;
    }
    
    public static float DirToDegree(Component from, Vector3 to)
    {
        return DirToRadian(from, to) * Mathf.Rad2Deg;
    }
    
    public static float DirToDegree(Vector3 from, Component to)
    {
        return DirToRadian(from, to) * Mathf.Rad2Deg;
    }
    
    public static float DirToDegree(Component from, Component to)
    {
        return DirToRadian(from, to) * Mathf.Rad2Deg;
    }
    #endregion

    #region DegreeBtweenTwoVector
    public static float DegreeBtweenTwoVector(Vector2 vA, Vector2 vB)
    {
        Vector2 dir = CalcDir(vA, vB);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    #endregion

    #region Distance
    public static float Distance(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }
    #endregion

    public static void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < 777; ++i)
        {
            int sour = Random.Range(0, list.Count);
            int dest = Random.Range(0, list.Count);

            T temp = list[dest];
            list[dest] = list[sour];
            list[sour] = temp;
        }
    }

    #region SpriteToTexture
    /*
    public static Texture ConvertSpriteToTexture(Sprite sprite)
    {
        try
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                int x = Mathf.FloorToInt(sprite.textureRect.x);
                int y = Mathf.FloorToInt(sprite.textureRect.y);
                int width = Mathf.FloorToInt(sprite.textureRect.width);
                int height = Mathf.FloorToInt(sprite.textureRect.height);

                Texture2D newText = new Texture2D(width, height);
                Color[] newColors = sprite.texture.GetPixels(x, y, width, height);

                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
        catch
        {
            return sprite.texture;
        }
    }
    */
    #endregion
}
