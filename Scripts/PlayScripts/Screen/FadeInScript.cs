using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    [SerializeField] private float fadeInTime;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = transform.Find("Panel").GetComponent<Image>();
        fadeInTime = 1f * fadeInTime / 10f;
        StartCoroutine("FadeIn");
    }

    //ロード画面からゲームが始まった時のフェードイン
    IEnumerator FadeIn()
    {
        for(var i = 1f; i >= 0; i -= 0.1f)
        {
            image.color = new Color(0f, 0f, 0f, i); //黒
            yield return new WaitForSeconds(fadeInTime);
        }
    }
}
