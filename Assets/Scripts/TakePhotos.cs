using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhotos : MonoBehaviour
{
     
    [SerializeField] private float photoTakeFrequency = 0.5f;
    private GameObject playerObject;
    private Transform player;

    void Start()
    {
        StartCoroutine(myWait());
        InvokeRepeating("TakePhotoRepeatedly", 0f, photoTakeFrequency);
    }
    IEnumerator myWait()
    {
        playerObject = null;
        while (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
            yield return null;
        } 
    }
    void TakePhotoRepeatedly()
    {
        StartCoroutine(takeAPhoto());
    }
    IEnumerator takeAPhoto()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        var currentRT = RenderTexture.active;
        camera.Render();
        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;
        RenderTexture.active = currentRT;
        byte[] bytes = image.EncodeToPNG();
        string playerX = (player.position.x).ToString("F2");
        string playerZ = (player.position.z).ToString("F2");
        float zRotation = player.transform.rotation.eulerAngles.y;
        string fileName = playerX + "X" + playerZ + "X" + zRotation.ToString("F2") + "X" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        File.WriteAllBytes(filePath, bytes);
        Destroy(rt);
        Destroy(image);
    }

}
