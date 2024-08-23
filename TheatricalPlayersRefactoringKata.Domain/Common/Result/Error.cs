using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Common.Result {
    public record Error {
        public static readonly Error None = new(string.Empty, ErrorType.Failure, string.Empty);

        private Error(string description, ErrorType errorType, string errorCode) {
            ErrorCode = errorCode;
            Description = description ?? string.Empty;
            ErrorType = errorType;
        }

        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }

        public static Error Failure(string description, string errorCode) => new(description, ErrorType.Failure, errorCode);
        public static Error BusinessRule(string description, string errorCode) => new(description, ErrorType.BusinessRule, errorCode);
        public static Error Validation(string description, string errorCode) => new(description, ErrorType.Validation, errorCode);
        public static Error NotFound(string description, string errorCode) => new(description,  ErrorType.NotFound, errorCode);
        public static Error Conflict(string description, string errorCode) => new(description, ErrorType.Conflict, errorCode);
    }
}
