using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Test
{
    [TestFixture]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //this list will be initialized only one time at the beggining
            //assume that it will not be modified by any test
            products = new List<LoanProduct>
            {
                new LoanProduct(1,"a",1),
                new LoanProduct(2,"b",2),
                new LoanProduct(3,"c",3),
            };

        }

        [SetUp]
        public void Setup() 
        {
            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [TearDown]
        public void TearDown()
        {
            //sut.Dispose();
            //sut.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //sut.Dispose();
        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicatedComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_withPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(1)
                                        .Property("ProductName").EqualTo("a")
                                        .And
                                        .Property("InterestRate").EqualTo(1)
                                        .And
                                        .Property("MonthlyRepayment").GreaterThan(0)
                                        );

            Assert.That(comparisons, Has.Exactly(1)
                                    .Matches<MonthlyRepaymentComparison>(
                                        mrc => mrc.ProductName == "a"
                                            && mrc.InterestRate == 1 
                                            && mrc.MonthlyRepayment > 0
                ));
        }

    }
}
