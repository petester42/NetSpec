namespace NetSpec.Matchers
{
    /// Implement this protocol to implement a custom matcher for Swift
    public interface Matcher<ValueType>
    {
        bool matches(Expression<ValueType> actualExpression, FailureMessage failureMessage);
        bool doesNotMatch(Expression<ValueType> actualExpression, FailureMessage failureMessage);
    }

    // private let dateFormatter: DateFormatter = {
    //     let formatter = DateFormatter()
    //     formatter.dateFormat = "yyyy-MM-dd HH:mm:ss.SSSS"
    //     formatter.locale = Locale(identifier: "en_US_POSIX")

    //     return formatter
    // }()

    // extension Date: NMBDoubleConvertible {
    //     public var doubleValue: CDouble {
    //         return self.timeIntervalSinceReferenceDate
    //     }
    // }

    // extension NSDate: NMBDoubleConvertible {
    //     public var doubleValue: CDouble {
    //         return self.timeIntervalSinceReferenceDate
    //     }
    // }

    // extension Date: TestOutputStringConvertible {
    //     public var testDescription: String {
    //         return dateFormatter.string(from: self)
    //     }
    // }

    // extension NSDate: TestOutputStringConvertible {
    //     public var testDescription: String {
    //         return dateFormatter.string(from: Date(timeIntervalSinceReferenceDate: self.timeIntervalSinceReferenceDate))
    //     }
}