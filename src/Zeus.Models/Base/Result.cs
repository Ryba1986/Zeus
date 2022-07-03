namespace Zeus.Models.Base
{
   public readonly struct Result
   {
      public bool IsSuccess { get; init; }
      public string Message { get; init; }
      public string Value { get; init; }

      public Result(bool isSuccess, string message, string value)
      {
         IsSuccess = isSuccess;
         Message = message;
         Value = value;
      }

      public static Result Success()
      {
         return new Result(true, string.Empty, string.Empty);
      }

      public static Result Success(string value)
      {
         return new Result(true, string.Empty, value);
      }

      public static Result Error(string message)
      {
         return new Result(false, message, string.Empty);
      }
   }
}
