﻿namespace AggregatorMicroService.Exceptions;

public class ModelException : Exception
{
    public ModelException(string message) : base(message)
    {
    }
}