
using FluentValidation.Results;

namespace ISUCorp.Test.Api.Domain.Notifier
{
    //Interface created for handling ValidationResult
    public interface INotifierService
    {
        ValidationResult ValidationResult();
    }
}
