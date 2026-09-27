# InfInt-Arbitrary-Precision-Integer-Class-in-C-
A simple big integer implementation in C#, built from scratch without relying on System.Numerics.BigInteger. Infint represents integers of arbitrary length as a list of decimal digits and supports comparison, addition, and subtraction — including correct handling of negative numbers.

# Features
* Arbitrary length: numbers are stored digit-by-digit, so size is limited only by available memory.
* Sign support: correctly parses and operates on negative numbers.
* Core operations:
  * CompareTo(Infint other) — compares two Infint values (-1, 0, 1)
  * Plus(Infint other) — addition
  * Minus(Infint other) — subtraction
  * ToString() — converts back to a standard decimal string
* Automatic normalization: leading zeros are stripped and -0 is normalized to 0 via RemoveZeroes().
# How it works

Internally, each Infint stores its digits least-significant-first in a List<int>:

# For example:
123 is stored as [3, 2, 1].

* Addition (Plus) — if both operands share the same sign, digits are added column-by-column with carry propagation. If signs differ, the operation is redirected to Minus on the appropriate operand.
* Subtraction (Minus) — if signs differ, it's redirected to Plus. If the result would be negative (the minuend is smaller), the operands are swapped and the sign of the result is flipped. Otherwise, digits are subtracted column-by-column with borrowing.
* Comparison (CompareTo) — first checks sign, then digit count, then compares digits from most-significant to least-significant.
