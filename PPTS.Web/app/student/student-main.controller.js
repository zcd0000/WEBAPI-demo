(function () {
    angular.module('ppts.student.controllers').controller('StudentMainController', ['$scope', '$uibModal', 'blockUI', 'studentService',
        function ($scope, $uibModal, blockUI, studentService) {
            var vm = {};
            $scope.vm = vm;

            //设置查询模式, '', 'simple', 'advance'三选一
            vm.SearchMode = '';
            vm.criteria = { Name: '', Code: '', TeacherZX: '', TeacherXG: '', Contact: '', PagedParam: { Page: 1 } };

            //简单查询按钮的事件
            $scope.simpleSearch = function () {
                vm.criteria.PagedParam.Page = 1;
                blockUI.start();
                //简单查询
                studentService.searchStudentList(vm.criteria, 'simple').then(function (data) {
                    if (data.Code == 0) {
                        vm.PagedList = data.Data.PagedList;
                        vm.criteria.PagedParam = data.Data.PagedList.PagedParam;
                        vm.SearchMode = 'simple';
                    } else {
                        bootbox.alert({ title: "错误", message: data.Message });
                    }
                    blockUI.stop();
                });
            };

            //高级搜索按钮的事件，弹出框，不直接搜索
            $scope.advanceSearch = function () {
                $uibModal.open({
                    templateUrl: '/app/student/main-advance-search.html',
                    controller: 'StudentMainAdvanceSearchController',
                    size: 'lg',
                    resolve: {
                        item: function () {
                            return 'mytest';
                        }
                    },
                    backdrop: 'static'
                }).result.then(function (advanceCriteria) {
                    vm.criteria.PagedParam.Page = 1;
                    blockUI.start();
                    //高级查询
                    studentService.searchStudentList({}, 'advance').then(function (data) {
                        if(data.Code == 0){
                            vm.PagedList = data.Data.PagedList;
                            vm.criteria.PagedParam = data.Data.PagedList.PagedParam;
                            vm.SearchMode = 'advance';
                        } else {
                            bootbox.alert({ title: "错误", message: data.Message });
                        }
                        blockUI.stop();
                    });
                });
            };

            $scope.pageChanged = function () {
                blockUI.start();
                studentService.searchStudentList(vm.criteria, vm.SearchMode).then(function (data) {
                    if (data.Code == 0) {
                        vm.PagedList = data.Data.PagedList;
                        vm.criteria.PagedParam = data.Data.PagedList.PagedParam;
                    } else {
                        bootbox.alert({ title: "错误", message: data.Message });
                    }
                    blockUI.stop();
                });
            };

            blockUI.start();
            //查询初始化页面数据
            studentService.getStudentMainIndex().then(function (data) {
                vm.PagedList = data.Data.PagedList;
                blockUI.stop();
            });
        }
    ]);
})();