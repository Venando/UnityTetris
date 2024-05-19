namespace Tetris.Messages
{
    public struct GameSpeedUpdatedMessage
    {
        public readonly float GameSpeedFactor;

        public GameSpeedUpdatedMessage(float gameSpeedFactor)
        {
            GameSpeedFactor = gameSpeedFactor;
        }
    }
}