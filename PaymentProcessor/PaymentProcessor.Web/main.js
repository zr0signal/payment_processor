//var myApp = angular.module('transactionApp', []);

//myApp.controller('TransactionController', ['$scope', function ($scope) {
//    $scope.master = {};

//    $scope.update = function (transaction) {
//        $scope.master = angular.copy(transaction);
//    };

//    $scope.reset = function () {
//        $scope.transaction = angular.copy($scope.master);
//    };

//    $scope.reset();
//}]);

$('.transaction-form').submit(function (e) {
    var transactionDetails = {
        CreditCardNumber: $('input[name=ShortNumber]').val(),
        CardHolder: $('input[name=CardHolder]').val(),
        ExpirationDate: $('input[name=ExpirationDate]').val(),
        SecurityCode: $('input[name=SecurityCode]').val(),
        Amount: $('input[name=Amount]').val()
    };

    $.post("http://localhost:50016/payment/process", transactionDetails, function (data) {
        $(".form_response").text(data.Message);
        $(".form_response").css('background', 'green');
    })
    .fail(function (xhr, status, error) {
        $(".form_response").text(error);
        $(".form_response").css('background', 'red');
    })
    .always(function () {
        $(".form_response").show();
    });;

    e.preventDefault();
});