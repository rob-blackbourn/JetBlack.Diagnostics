# JetBlack.Diagnostics

## Introduction

This library provides some support code for supplimenting the standard System.Diagnostics assembly.

## Performance counters

A framework for performance counters addresses three issues: ease of development, testability, useability.

### Ease of development & testability

When writing code which contains performance counters, development is hampered by having to register performance
counters before debugging the code. The same problem arises in testing.
This issue is addressed by creating an interface IPerformanceCounter, a factory
IPerformanceCounterFactory. This means that in development and test we can mock the counters
before installing them in production code.

### Useability

Under the hood a performance counter is essentially a long, a timestamp, and a type. The type determines the calculation
performed when the counter is sampled. The system also understands how to combine two counters of specific types
(usually a counter and a base) to provide more sophisticated statics like average time spent performing an
operation. They need to be registered before use; and in the case of composite counters we must ensure the counter and base
counter is registered consequtively and in order.

An example of this is the integer average timeer. It consists of a numerator of type AverageCouter32 and a denominator of
AverageBase. The average time is achieved by incrementing tyhe numerator by the elapsed ticks, and the denominator by one.
When registered we must ensure that the numerator is created before the denominator which must immediately succeed it.

The IntAverageTimer class provides this functionality. The increment method with the supplied elapsed ticks updates the
numerator by the ticks an the denominator by one. A static method can create the counter data for an installer. As the method
uses a factory interface to create it's counters, development and testing are not hampered by the need for registration.