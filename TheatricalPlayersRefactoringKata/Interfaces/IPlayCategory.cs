﻿public interface IPlayCategory
{
    decimal CalculateAmount(int seats, int performanceId);
    int CalculatePoints(int seats);
}