using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImg : MonoBehaviour
{
    [SerializeField] private Button _downloadButton;
    [SerializeField] private string _imageUrl = "https://example.com/image.png";
    [SerializeField] private Image _targetImage;

    private void Start()
    {
        _downloadButton.onClick.AddListener(DownloadImage);
    }

    private void DownloadImage()
    {
        StartCoroutine(DownloadImageCoroutine(_imageUrl));
    }

    private IEnumerator DownloadImageCoroutine(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    _targetImage.sprite = sprite;
                if(!_targetImage.gameObject.activeInHierarchy)
                {
                    _targetImage.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("Failed to download image: " + request.error);
            }
        }
    }
}
