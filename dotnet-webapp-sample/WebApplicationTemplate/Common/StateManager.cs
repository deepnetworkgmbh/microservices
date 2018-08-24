namespace WebApplicationTemplate.Common
{
    public static class StateManager
    {
        public static State CurrentState { get; private set; } = State.Paused;

        public static void SetHealthy()
        {
            CurrentState = State.Healthy;
        }

        public static void Pause()
        {
            CurrentState = State.Paused;
        }

        public static void RequestRestart()
        {
            CurrentState = State.RequiresRestart;
        }
    }
}
