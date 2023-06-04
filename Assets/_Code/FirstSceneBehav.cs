// using Anim;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class FirstSceneBehav : MonoBehaviour
{
    private void Start()
    {
        var goButton_StartGame = GameObject.Find("/Canvas/Button_StartGame");
        goButton_StartGame.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                // var myTween = new MyTween();
                // StartCoroutine(myTween.DoScale_XC(goButton_StartGame.transform));

                // myTween.DoScale(goButton_StartGame.transform);

                // C# 扩展机制
                //goButton_StartGame
                //    .transform
                //    .DoScale()
                //    .onCompleted(() =>
                //    {
                //        //跳转到游戏场景
                //        SceneManager.LoadScene("GameScene");
                //    });

                // 使用 DoTween 缓动插件
                goButton_StartGame.transform
                    .DOShakeScale(1.5f, 0.5f)
                    .OnComplete(() =>
                    {
                        goButton_StartGame.transform.localScale = Vector3.one;

                        //跳转到游戏场景
                        SceneManager.LoadScene("GameScene");
                    });
            });
    }

}
