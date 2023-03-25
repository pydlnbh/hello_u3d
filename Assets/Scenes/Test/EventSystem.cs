using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scenes
{
    public delegate void EventListener(); // 函数指针

    public partial class EventSystem
    {
        public EventListener EventListener
        {
            get;
            set;
        }

        public void DoEvent()
        {
            //if (null == EventListener) 
            //{
            //    EventListener();
            //}

            EventListener ?. Invoke();
        }
    }
}
