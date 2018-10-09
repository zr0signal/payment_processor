# Payment processor

A web service providing a payment/transaction processing functionality.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

```
Visual Studio 2017 with the desktp and web developement features installed
```

### Installing

```
1. Clone repository
2. Open the 'PaymentProcessor.sln' solution file in Visual Studio
3. Build the solution
4. Run the 'PaymentProcessor.Api' project
5. (Optional) Run the 'PaymentProcessor.Web' project (while 'PaymentProcessor.Api' is also running) if you want to use the form view.
```
See [this](https://msdn.microsoft.com/en-us/library/ms165413.aspx) for running multiple projects at the same time.

### Request Example
Type: POST

Url: /payment/process

Body (json):

```
{
  "CreditCardNumber": "1234",
  "CardHolder": "Your Name",
  "ExpirationDate": "2019-01-01",
  "SecurityCode": "212",
  "Amount": 233442
}
```

### Response Example
```
{
    Message: "OK"
}
```

## Running the tests

Automated tests can be found in the 'DellTest.Customers.Service.Tests' project. To run them, use the 'Test Explorer' window, available in Visual Studio.
