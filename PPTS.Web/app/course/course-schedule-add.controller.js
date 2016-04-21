angular.module('ppts.course.controllers').controller('CourseMainAddScheduleController', ['$scope', '$uibModalInstance',
    function ($scope, $uibModalInstance) {
        $scope.ok = function () {
            $uibModalInstance.close('test');
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
]);