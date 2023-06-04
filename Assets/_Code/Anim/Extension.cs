using UnityEngine;

namespace Anim
{
    public static class Extension
    {
        /// <summary>
        /// 相当于扩展 Transform 类,
        /// 不破坏原来的代码
        /// 给其增加一个 DoScale 函数
        /// </summary>
        /// <param name="t"></param>
        public static MyTween DoScale(this Transform t)
        {
            var myTween = new MyTween();
            myTween.DoScale(t);

            return myTween;
        }
    }
}
