using System.Collections.Generic;
using System.Globalization;
using Localizy.Storage;
using Xunit;

namespace Localizy.Tests
{
    public class UpdateTextTests
    {
        private readonly LocalizationProvider _provider;
        private InMemoryLocalizationStorageProvider _storageProvider;
        private Dictionary<CultureInfo, List<LocalString>> _data;

        public UpdateTextTests()
        {
            _data = new Dictionary<CultureInfo, List<LocalString>>()
            {
                {
                    new CultureInfo("en"), new List<LocalString>()
                    {
                        new LocalString("TestTranslations.General:Test1", "Test1en")
                    }
                },
                {
                    new CultureInfo("fr"), new List<LocalString>()
                    {
                        new LocalString("TestTranslations.General:Test1", "Test1fr")
                    }
                }
            };

            _storageProvider = new InMemoryLocalizationStorageProvider("1", _data);

            _provider = new LocalizationProvider(_storageProvider)
            {
                CurrentCultureFactory = () => new CultureInfo("en")
            };
        }

        [Fact]
        public void UpdateTextForASpecificKeyAndCulture_ShouldUpdateTheTranslationForTheCultureSpecified()
        {
            var text1en = _provider.GetText("TestTranslations.General:Test1", new CultureInfo("en"));

            _provider.UpdateText(new LocalizationKey("TestTranslations.General:Test1"), new CultureInfo("en"), "Test1en-updated");

            var text2en = _provider.GetText("TestTranslations.General:Test1", new CultureInfo("en"));
            Assert.Equal("Test1en", text1en);
            Assert.Equal("Test1en-updated", text2en);
        }
    }
}