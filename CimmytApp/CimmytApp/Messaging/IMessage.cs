﻿namespace CimmytApp.Messaging
{
    public interface IMessage
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}