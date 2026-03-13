using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitPro_DBDataDrivenFail
{
    [TestFixture]   
    public class DBDataSet
    {
        IWebDriver _driver;
        [SetUp]
        public void DriverInit()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
            _driver.Manage().Window.Maximize();
        }
        public static IEnumerable<TestCaseData> LoginDetails()
        {
            var data = DBDataSource.GetDataSource();
            foreach(var records in data)
            {
                yield return new TestCaseData(records.Username, records.Password);
            }
        }
        [Test,TestCaseSource(nameof(LoginDetails))]
        public void TC_DBFaildLogin(string _username, string _password)
        {
            _driver.FindElement(By.Id("username")).SendKeys(_username);
            _driver.FindElement(By.Id("password")).SendKeys(_password);
            _driver.FindElement(By.Id("login")).Click();
            NUnit.Framework.Assert.That(_driver.FindElement(By.Id("username_span")).Text, Is.EqualTo("Invalid Login details or Your Password might have expired. Click here to reset your password"));
        }
        [TearDown]
        public void DriverClose()
        {
            if(_driver !=null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
