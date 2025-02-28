using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SpanishPointAssignment.Pages
{
    public class MatchingEngineHomePage
    {
        private IWebDriver Driver;
        private WebDriverWait wait;


        public MatchingEngineHomePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        private By ModulesMenu => By.CssSelector("a[href='https://www.matchingengine.com/#Modules']");
        private By RepertoireModule => By.XPath("//a[@href='https://www.matchingengine.com/repertoire-management-module/' and text()='Repertoire Management Module']");
        private By AdditionalFeaturesSection => By.XPath("//h2[@class='vc_custom_heading' and contains(text(), 'Additional Features')]");
        private By ProductsSupportedButton => By.XPath("//a[@href='#1661350017393-4859bb9f-5341c79e-be34']//span[text()='Products Supported']");
        private By ProductListHeading => By.XPath("//h3[text()='There are several types of Product Supported:']");
        private By ProductList => By.XPath("//h3[text()='There are several types of Product Supported:']/following-sibling::div//ul");
        private By ProductListItems => By.XPath("//h3[text()='There are several types of Product Supported:']/following-sibling::div//ul//li");


        public void ClickModulesMenu() => wait.Until(ExpectedConditions.ElementToBeClickable(ModulesMenu)).Click();
        public void ClickRepertoireModule() => wait.Until(ExpectedConditions.ElementToBeClickable(RepertoireModule)).Click();
        public void ScrollToAdditionalFeatures()
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(AdditionalFeaturesSection));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        public void ClickProductsSupported() => wait.Until(ExpectedConditions.ElementToBeClickable(ProductsSupportedButton)).Click();
        public bool IsProductListVisible()
        {
            var productList = wait.Until(ExpectedConditions.ElementIsVisible(ProductList));
            if (productList.Displayed)
            {
                var listItems = productList.FindElements(ProductListItems);
                return listItems.Count > 0;
            }
            return false;
        }

        public bool VerifyProductListContainsExpectedItems()
        {
            var productList = wait.Until(ExpectedConditions.ElementIsVisible(ProductList));

            if (productList.Displayed)
            {
                var productListItems = productList.FindElements(ProductListItems);

                Console.WriteLine("Products in the list:");
                foreach (var productItem in productListItems)
                {
                    Console.WriteLine(productItem.Text.Trim()); 
                }
                var expectedProducts = new[] { "Cue Sheet / AV Work", "Recording", "Bundle", "Advertisement" };

                foreach (var expectedProduct in expectedProducts)
                {
                    bool isProductFound = false;
                    foreach (var productItem in productListItems)
                    {
                        if (productItem.Text.Contains(expectedProduct)) 
                        {
                            isProductFound = true;
                            break; 
                        }
                    }
                    if (!isProductFound)
                    {
                        Console.WriteLine($"Product '{expectedProduct}' not found in the list.");
                        return false;
                    }
                }
                Console.WriteLine("All expected products are present in the list.");
                return true;
            }
            Console.WriteLine("Product list is not visible.");
            return false;
        }

    }
}
