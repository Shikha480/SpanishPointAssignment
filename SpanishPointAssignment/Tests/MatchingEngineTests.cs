using NUnit.Framework;
using SpanishPointAssignment.Pages;


namespace SpanishPointAssignment.Tests
{
        public class MatchingEngineTests : BaseTest
    {
        private MatchingEngineHomePage matchingEngineHomePage=null!;

        public void SetupTest()
        {
            matchingEngineHomePage = new MatchingEngineHomePage(Driver);
        }

        public void TestSupportedProductsList()
        {
            matchingEngineHomePage.ClickModulesMenu();
            matchingEngineHomePage.ClickRepertoireModule();
            matchingEngineHomePage.ScrollToAdditionalFeatures();
            matchingEngineHomePage.ClickProductsSupported();
            Assert.That(matchingEngineHomePage.IsProductListVisible(), Is.True, "Product list is not visible.");
            Assert.That(matchingEngineHomePage.VerifyProductListContainsExpectedItems(), Is.True, "The product list does not contain the correct products.");
        }
    }
}

