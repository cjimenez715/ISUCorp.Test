
using FluentValidation.Results;

namespace ISUCorp.Test.Api.Domain.Notifier
{
    public interface INotifierService
    {
        ValidationResult ValidationResult();
    }
}
