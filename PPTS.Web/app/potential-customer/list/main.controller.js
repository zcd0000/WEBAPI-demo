(function () {
    angular.module('ppts.potential-customer.controllers').controller('PotentialCustomerMainController', ['$scope', '$uibModal', 'blockUI',
        function ($scope, $uibModal, blockUI) {
            var vm = {};
            $scope.vm = vm;

            vm.test1 = "潜在客户列表页面";
        }
    ]);
})();