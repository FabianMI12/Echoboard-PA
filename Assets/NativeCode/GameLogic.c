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

EXPORT int GetRemainingSteps(int totalSteps, int currentStep)
{
    int remaining = totalSteps - currentStep;

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

EXPORT int GetNextPlayerIndex(int currentPlayerIndex, int totalPlayers)
{
    if (totalPlayers <= 0)
        return 0;

    return (currentPlayerIndex + 1) % totalPlayers;
}

EXPORT int ShouldKillPawnByDistance(float ax, float ay, float bx, float by, float killDistance)
{
    float dx = ax - bx;
    float dy = ay - by;

    float distanceSquared = dx * dx + dy * dy;
    float killDistanceSquared = killDistance * killDistance;

    if (distanceSquared <= killDistanceSquared)
        return 1;

    return 0;
}