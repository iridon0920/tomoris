using System;
public class Score
{
    public double TotalPoints { get; private set; } = 0;

    public void AddPointFromErasedLines(int erasedLines)
    {
        TotalPoints += Math.Pow(2, erasedLines - 1) * 100;
    }
}
