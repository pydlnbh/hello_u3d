using UnityEngine;

namespace Anim
{
    public sealed class MyTweenComponent : MonoBehaviour
    {
        private static MyTweenComponent INSTANCE;

        public static MyTweenComponent TheInstance()
        {
            if (null == INSTANCE)
            {
                var goTemp = new GameObject();
                goTemp.name = "[MyTween]";
                GameObject.DontDestroyOnLoad(goTemp);
                INSTANCE = goTemp.AddComponent<MyTweenComponent>();
            }

            return INSTANCE;
        }
    }
}
