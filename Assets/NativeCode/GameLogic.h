#ifndef GAME_LOGIC_H
#define GAME_LOGIC_H

#ifdef _WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT
#endif

EXPORT int AddScore(int currentScore, int points);
EXPORT int GetRemainingCorrectAnswers(int maxQuestions, int correctAnswers);
EXPORT int GetRemainingSteps(int totalSteps, int currentStep);
EXPORT int ClampInt(int value, int min, int max);
EXPORT int ShouldMovePawn(int isCorrect);
EXPORT int GetTotalAnswered(int correctAnswers, int wrongAnswers);
EXPORT int GetNextPlayerIndex(int currentPlayerIndex, int totalPlayers);
EXPORT int ShouldKillPawnByDistance(float ax, float ay, float bx, float by, float killDistance);

#endif