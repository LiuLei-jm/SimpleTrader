﻿using System.Runtime.Serialization;

namespace SimpleTrader.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; set; }
        public UserNotFoundException(string username)
        {
            Username = username;
        }

        public UserNotFoundException(string? message, string username) : base(message)
        {
            Username = username;
        }

        public UserNotFoundException(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
