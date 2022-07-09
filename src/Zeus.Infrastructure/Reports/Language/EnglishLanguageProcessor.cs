using Zeus.Infrastructure.Reports.Language.Base;

namespace Zeus.Infrastructure.Reports.Language
{
   internal sealed class EnglishLanguageProcessor : ILanguageProcessor
   {
      public string FIleName { get; init; }

      public EnglishLanguageProcessor()
      {
         FIleName = "Report";
      }
   }
}
