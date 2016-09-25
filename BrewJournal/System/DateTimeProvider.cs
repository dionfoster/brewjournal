﻿using System;

namespace BrewJournal.System
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}