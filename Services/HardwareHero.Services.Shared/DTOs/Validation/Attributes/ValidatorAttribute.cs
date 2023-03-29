﻿namespace HardwareHero.Services.Shared.DTOs.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ValidatorAttribute : Attribute
    {
        public Type DtoType { get; private set; }
        public ValidatorAttribute(Type dtoType)
        {
            DtoType = dtoType;
        }
    }
}
