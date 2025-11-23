using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public static class Utils
{
    public static IEnumerator LoadSprite(string url, Image target)
    {
        if (string.IsNullOrEmpty(url)) yield break;

        using var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var tex = DownloadHandlerTexture.GetContent(www);
            target.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        }
    }

    public static Transform GetClosestSlot(Vector3 position, Transform parent)
    {
        Transform closest = null;
        float minDist = float.MaxValue;

        foreach (Transform child in parent)
        {
            float dist = Vector3.Distance(position, child.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = child;
            }
        }

        return closest;
    }
}