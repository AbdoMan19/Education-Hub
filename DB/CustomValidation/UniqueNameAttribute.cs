using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MVC_Task.UnitOfWork;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UniqueAttribute<TEntity> : ValidationAttribute where TEntity : class
{
    private readonly string _propertyName;

    public UniqueAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        
        var unitOfWork = (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork));
        
        // Get the dynamic property value
        var propertyValue = value?.ToString();

        if (string.IsNullOrWhiteSpace(propertyValue))
        {
            return ValidationResult.Success; 
        }

        // Dynamically access the property of the entity based on the provided property name
        var existingEntity = unitOfWork.Repository<TEntity>()
            .FindBy(e => EF.Property<string>(e, validationContext.MemberName) == propertyValue)
            .FirstOrDefault();

        if (existingEntity != null)
        {
            return new ValidationResult(ErrorMessage ?? $"{_propertyName} '{propertyValue}' already exists.");
        }

        return ValidationResult.Success;
    }
}
