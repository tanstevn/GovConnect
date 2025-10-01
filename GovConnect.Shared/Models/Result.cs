using FluentValidation.Results;

namespace GovConnect.Shared.Models {
    /// <summary>
    /// The base type of result.
    /// This does not include any data.
    /// </summary>
    public class UnitResult {
        public IEnumerable<string>? Errors { get; set; }
        public IEnumerable<ValidationFailure>? Failures { get; set; }
        public bool IsSuccess {
            get {
                return !Errors.Any()
                    && !Failures.Any();
            }
        }

        public static UnitResult Success() {
            return new();
        }

        public static UnitResult Error(string error) {
            return new() {
                Errors = new List<string> {
                    error
                }
            };
        }

        public static UnitResult MultipleErrors(IEnumerable<string> errors) {
            return new() {
                Errors = errors
            };
        }

        public static UnitResult ValidationErrors(IEnumerable<ValidationFailure> failures) {
            return new() {
                Failures = failures
            };
        }
    }

    /// <summary>
    /// A subclass of the base type of result.
    /// This includes data.
    /// </summary>
    /// <typeparam name="TData">Data to return</typeparam>
    public class Result<TData> : UnitResult {
        public TData? Data { get; set; }
        public static Result<TData> Success(TData data) {
            return new() {
                Data = data
            };
        }

        public new static Result<TData> Error(string error) {
            return new() {
                Errors = new List<string> {
                    error
                }
            };
        }

        public new static Result<TData> MultipleErrors(IEnumerable<string> errors) {
            return new() {
                Errors = errors
            };
        }
    }

    /// <summary>
    /// A result subclass that is used for pagination
    /// </summary>s
    /// <typeparam name="TData">Data to return</typeparam>
    public class PaginatedResult<TData> : Result<TData> {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int ResultsCount { get; set; }
        public int TotalResultsCount { get; set; }
        public int TotalPages { get; set; }
        public new IEnumerable<TData>? Data { get; set; }
        public static PaginatedResult<TData> Success(IEnumerable<TData> data) {
            return new() {
                Data = data
            };
        }

        public new static PaginatedResult<TData> Error(string error) {
            return new() {
                Errors = new List<string> {
                    error
                }
            };
        }

        public new static PaginatedResult<TData> MultipleErrors(IEnumerable<string> errors) {
            return new() {
                Errors = errors
            };
        }
    }
}
