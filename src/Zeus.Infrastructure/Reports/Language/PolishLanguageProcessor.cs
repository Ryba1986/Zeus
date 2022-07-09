using Zeus.Infrastructure.Reports.Language.Base;

namespace Zeus.Infrastructure.Reports.Language
{
   internal sealed class PolishLanguageProcessor : ILanguageProcessor
   {
      public string FIleName { get; init; }

      public PolishLanguageProcessor()
      {
         FIleName = "Raport";
      }
   }
}
