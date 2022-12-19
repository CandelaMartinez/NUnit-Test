using Loans.Domain.Applications;
using NUnit.Framework.Constraints;

namespace Loans.Test
{
    public class MonthlyRepaymentGreaterThanZeroConstraint : Constraint
    {
        public string _expectedProductName { get; }
        public decimal _expectedInterestRate { get; }

        public MonthlyRepaymentGreaterThanZeroConstraint(string expectedProductName,
                                                         decimal expectedInterestRate)
        {
            _expectedProductName = expectedProductName;
            _expectedInterestRate = expectedInterestRate;

        }
    public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            MonthlyRepaymentComparison comparison = actual as MonthlyRepaymentComparison;

            if (comparison is null)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Error);

            }

            if(comparison.InterestRate == _expectedInterestRate
                            && comparison.ProductName == _expectedProductName
                            && comparison.MonthlyRepayment > 0)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            }

            return new ConstraintResult(this, actual, ConstraintStatus.Failure);

        }
    }
}
