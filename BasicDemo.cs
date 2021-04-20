using Applitools;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace VisualTesting
{

    [TestFixture]
    public class BasicDemo
    {
        private EyesRunner runner;
        private Eyes eyes;

        private IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            // Use Chrome browser
            driver = new ChromeDriver();

            //Initialize the Runner for your test.
            runner = new ClassicRunner();

            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes(runner);

            SetUp();
        }


        [Test]
        public void BasicTest()
        {
            // Start the test by setting AUT's name, window or the page name that's being tested, viewport width and height
            eyes.Open(driver, "Demo App - csharp", "Smoke Test", new Size(800, 600));
                        
            driver.Url = "https://demo.applitools.com/";
            //driver.Url = "https://demo.applitools.com/index_v2.html";

            // https://applitools.com/docs/topics/sdk/the-eyes-sdk-check-fluent-api.html?Highlight=fluent%20api
            eyes.Check(Target.Window().Fully().WithName("Login Window"));

            // This will create a test with two test steps.
            driver.FindElement(By.Id("log-in")).Click();
            
            // Visual checkpoint #2 - Check the app page.
            eyes.Check(Target.Window().Fully().WithName("App Window"));

            // End the test.
            eyes.CloseAsync();
        }       

        [TearDown]
        public void AfterEach()
        {
            // Close the browser.
            driver.Quit();

            // If the test was aborted before eyes.close was called, ends the test as aborted.
            eyes.AbortIfNotClosed();

            //Wait and collect all test results
            // we pass false to this method to suppress the exception that is thrown if we
            // find visual differences
            TestResultsSummary allTestResults = runner.GetAllTestResults(false);

            // Print results
            Console.WriteLine(allTestResults);
        }

        private void SetUp()
        {
            // Initialize the eyes configuration.
            Applitools.Selenium.Configuration config = new Applitools.Selenium.Configuration();

            // Add this configuration if your tested page includes fixed elements.
            //config.setStitchMode(StitchMode.CSS);


            // You can get your api key from the Applitools dashboard
            config.SetApiKey("API Key");

            // set new batch
            config.SetBatch(new BatchInfo("Demo batch"));

            // set the configuration to eyes
            eyes.SetConfiguration(config);
        }

    }
}
