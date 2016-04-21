angular.module('ppts.student.controllers').controller('StudentMainAdvanceSearchController', ['$scope', '$uibModalInstance',
    function ($scope, $uibModalInstance) {
        $scope.ok = function () {
            $uibModalInstance.close('test');
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
]);