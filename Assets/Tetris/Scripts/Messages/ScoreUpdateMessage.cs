namespace Tetris.Messages
{
    public struct ScoreUpdateMessage
    {
        public int Score;

        public ScoreUpdateMessage(int score)
        {
            Score = score;
        }
    }
}