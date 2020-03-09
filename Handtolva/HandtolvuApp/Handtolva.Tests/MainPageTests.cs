using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Handtolva.Tests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class MainPageTests
    {
        IApp app;
        Platform platform;

        public MainPageTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void InsertingTextShouldUpdateModel()
        {
            const string input = "12345";
            // Arrange
            app.EnterText("inputEntry", input);

            // Act

            // Assert
            //AppResult[] testResult = app.Query(c => c.Class("inputEntry").Text(input));
            var testResult = app.Query("inputEntry").First(res => res.Text == input);

            Assert.IsTrue(testResult != null, "Input is not the same!");
        }
    }
}
