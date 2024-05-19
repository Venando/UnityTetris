namespace Tetris.Messages
{
    public struct ScoreUpdateMessage
    {
        public readonly int Score;

        public ScoreUpdateMessage(int score)
        {
            Score = score;
        }
    }
}