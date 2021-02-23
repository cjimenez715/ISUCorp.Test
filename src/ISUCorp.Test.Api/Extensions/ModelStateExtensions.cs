using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ISUCorp.Test.Api.Extensions
{
    //Class created for update ModelState 
    public static class ModelStateExtensions
    {
        private const string KEY = "Domain";
        //Adding ValidationResult to ModelState
        public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
                validationResult.AddToModelState(modelState, KEY);
        }

        // Getting All Errors from ModelState
        public static ValidationProblemDetails GetValidationProblemDetails(this ModelStateDictionary modelState)
        {
            return new ValidationProblemDetails(modelState);
        }

    }
}
