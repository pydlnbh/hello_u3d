using System;
using System.Collections;
using UnityEngine;

namespace Anim
{
    /// <summary>
    /// 缓动动画类
    /// </summary>
    public sealed class MyTween
    {
        // 缓动动画完成后回调函数
        private Action _onCompleteAct;

        public void DoScale(Transform t)
        {
            MyTweenComponent.TheInstance().StartCoroutine(DoScale_XC(t));
        }

        /// <summary>
        /// 协程方式执行缩放
        /// </summary>
        /// <param name="t">按钮位置</param>
        /// <returns></returns>
        private IEnumerator DoScale_XC(Transform t)
        {
            if (null == t)
            {
                yield break;
            }

            for (var i = 0; i < 16; i++)
            {
                t.localScale += (Vector3.right + Vector3.up) * (Time.deltaTime * 2f);
                yield return 1;
            }

            for (var i = 0; i < 16; i++)
            {
                t.localScale -= (Vector3.right + Vector3.up) * (Time.deltaTime * 2f);
                yield return 1;
            }

            t.localScale = Vector3.one;

            _onCompleteAct?.Invoke();
        }

        public void onCompleted(Action val)
        {
            _onCompleteAct = val;
        }
    }
}
