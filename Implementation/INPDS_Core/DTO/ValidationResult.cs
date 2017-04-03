using System.Collections.Generic;

namespace INPDS_Core.DTO
{
    /// <summary>
    ///     Immutable class containing validation result. New instances are created via static factory methods.
    /// </summary>
    public class ValidationResult
    {
        private static readonly ValidationResult OkResult = new ValidationResult();
        private readonly List<string> _validationMessages = new List<string>();

        private ValidationResult()
        {
        }

        public bool IsValid
        {
            get { return _validationMessages.Count <= 0; }
        }

        public IEnumerable<string> GetMessages
        {
            get { return _validationMessages.AsReadOnly(); }
        }

        public ValidationResult JoinResults(ValidationResult validationResult)
        {
            var result = new ValidationResult();
            result._validationMessages.AddRange(_validationMessages);
            result._validationMessages.AddRange(validationResult._validationMessages);
            return result;
        }

        public static ValidationResult Ok()
        {
            return OkResult;
        }

        public static ValidationResult Error(params string[] messages)
        {
            var validationResult = new ValidationResult();
            validationResult._validationMessages.AddRange(messages);
            return validationResult;
        }
    }
}