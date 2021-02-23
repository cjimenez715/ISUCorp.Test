using FluentValidation.Results;

namespace ISUCorp.Test.Api.Domain.Notifier
{
    //Class created for handling Services ValidationResult
    public abstract class NotifierService : INotifierService
    {
        private readonly ValidationResult _validationResult;

        public abstract ValidationResult ValidationResult();

        protected NotifierService()
        {
            _validationResult ??= new ValidationResult();
        }

        public void AddValidationFailure(string message)
        {
            _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        public ValidationResult GetValidationResult()
        {
            return _validationResult;
        }
    }
}
