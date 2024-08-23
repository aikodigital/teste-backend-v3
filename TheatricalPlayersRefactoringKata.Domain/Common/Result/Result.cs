

namespace TheatricalPlayersRefactoringKata.Domain.Common.Result {
    public class Result<T> {

        private Result(T value) {
            IsSuccess = true;
            Value = value;
            Error = Error.None;
        }

        private Result(Error error, T genericError) {
            IsSuccess = false;
            Error = error;
            GenericError = genericError;
        }

        public bool IsSuccess { get; }
        public T Value { get; }
        public T GenericError { get; }
        public Error Error { get; }

        public static Result<T> Success(T value)  => new(value);
        public static Result<T> Failure(Error error, T genericError)  => new(error, genericError);
    }
}
