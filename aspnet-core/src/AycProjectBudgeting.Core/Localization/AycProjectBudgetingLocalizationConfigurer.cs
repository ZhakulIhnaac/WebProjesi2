using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AycProjectBudgeting.Localization
{
    public static class AycProjectBudgetingLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AycProjectBudgetingConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AycProjectBudgetingLocalizationConfigurer).GetAssembly(),
                        "AycProjectBudgeting.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
