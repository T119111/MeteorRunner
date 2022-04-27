using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private GameObject loadUI;
    [SerializeField] private Slider slider;
    //public int index;

    //スタートボタンを押したらこの関数を呼び出し、ロード画面を表示
    public void NextScene()
    {
        loadUI.SetActive(true);
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        //ロードが完了したら、プレイシーンに移動
        //index = PlayerPrefs.GetInt("Stage");
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1 + ChooseStage.index);
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
