using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ISUCorp.Test.Api.Extensions
{
    public static class ModelStateExtensions
    {
        private const string KEY = "Domain";

        /// <summary>
        /// Add ValidationResult to ModelState
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="validationResult"></param>
        public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
                validationResult.AddToModelState(modelState, KEY);
        }

        /// <summary>
        /// Get All Errors from ModelState
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ValidationProblemDetails GetValidationProblemDetails(this ModelStateDictionary modelState)
        {
            return new ValidationProblemDetails(modelState);
        }

    }
}
