namespace Assets.Scenes
{
    public partial class EventSystem
    {
        public void SayHello()
        {
            EventListener?.Invoke();
        }
    }
}
