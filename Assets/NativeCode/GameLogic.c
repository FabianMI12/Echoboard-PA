#include "GameLogic.h"

EXPORT int AddScore(int currentScore, int points)
{
    return currentScore + points;
}

EXPORT int GetRemainingCorrectAnswers(int maxQuestions, int correctAnswers)
{
    int remaining = maxQuestions - correctAnswers;

    if (remaining < 0)
        return 0;

    return remaining;
}

EXPORT int ClampInt(int value, int min, int max)
{
    if (value < min)
        return min;

    if (value > max)
        return max;

    return value;
}

EXPORT int ShouldMovePawn(int isCorrect)
{
    if (isCorrect == 1)
        return 1;

    return 0;
}

EXPORT int GetTotalAnswered(int correctAnswers, int wrongAnswers)
{
    return correctAnswers + wrongAnswers;
}