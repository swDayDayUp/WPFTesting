using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEOTesting;

namespace UnitTestProjectSEO
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToStringCanConcatAllItems4NonEmptyList()
        {
            //Arrange
            var ranklist = new List<string>
            {
                "1", "2", "3"
            };
            var tesSEO = new SEOSummary(ranklist);
            

            //Act
            string outputText = tesSEO.ToString();

            //Assert
            Assert.AreEqual(outputText, "1,2,3");
        }

        [TestMethod]
        public void ToStringReturn0ForEmptyList()
        {
            //Arrange
            var tesSEO = new SEOSummary(new List<string>());
           
            //Act
            string outputText = tesSEO.ToString();

            //Assert
            Assert.AreEqual(outputText, "0");
        }


        [TestMethod]
        public void FindAllMatches()
        {
            //Arrange
            var tesSEO = new SEOSummary("https://www.google.com.au/search?num=100&q=conveyancing+software", @"(?i)<cite[^>]*>(.*)</cite>", "smokeball.com.au");
            tesSEO.ResponseText = "<cite>smokeball.com.au</cite>abdc1234";
            


            //Act
           tesSEO.FindAllMatches();

            //Assert
            Assert.AreEqual(tesSEO.ToString(), "1");
           
        }
    }
}
