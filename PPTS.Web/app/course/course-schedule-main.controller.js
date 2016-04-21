(function () {
    angular.module('ppts.course.controllers').controller('CourseScheduleMainController', ['$scope', '$compile', '$uibModal', 'blockUI', 'courseService', '$http',
        function ($scope, $compile, $uibModal, blockUI, courseService, $http) {
            var vm = {};
            $scope.vm = vm;

            vm.selectedEvent = null;
            vm.selectedEventElement = null;

            //设置查询模式, '', 'simple', 'advance'三选一
            vm.SearchMode = 'simple';
            vm.criteria = { Start: '2016-03-01', End: '2016-04-30' };

            vm.Teachers = [{ text: 'abc' }];

            //配置日程组件
            vm.uiConfig = {
                calendar: {
                    lang: 'zh-cn',
                    allDaySlot: false,
                    height: 730,//1100,
                    slotLabelFormat: 'HH:mm',
                    minTime: '06:00:00',
                    maxTime: '22:00:00',
                    timeFormat: 'HH:mm',
                    editable: false,
                    selectable: true,
                    selected: true,
                    defaultView: 'agendaWeek',//'agendaWeek',
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
                    },
                    eventRender: function (event, element, view) {
                        //var html = '<div uib-dropdown class="mcs-calendar-event" ng-class="{\'mcs-calendar-event-selected\': vm.selectedEvent.selected==' + event.selected + '}" uib-tooltip="' + event.startText + '-' + event.endText + '\r\n' + event.title + '" tooltip-append-to-body="true">\
                        //    <div ng-click="toggleEventSelected(\'' + event.id + '\')">' + event.title + '</div>\
                        //    <div style="position: absolute; float: right; right:5px; top:0;" uib-dropdown-toggle ng-show="vm.selectedEvent.selected==' + event.selected + '">\
                        //        <span class="caret"></span>\
                        //        <span class="sr-only">Split button!</span>\
                        //    </div>\
                        //    <ul uib-dropdown-menu role="menu" aria-labelledby="split-button" style="z-index:100;">\
                        //        <li role="menuitem"><a href="javascript:alert(\'复制\');"><i class="ace-icon fa fa-floppy-o bigger-120 green"></i> 复制</a></li>\
                        //        <li role="menuitem"><a href="javascript:alert(\'调课\');"><i class="ace-icon fa fa-random bigger-120 green"></i> 调课</a></li>\
                        //        <li class="divider"></li>\
                        //        <li role="menuitem"><a href="javascript:alert(\'取消\');"><i class="ace-icon fa fa-trash-o bigger-120 orange"></i> 取消</a></li>\
                        //    </ul>\
                        //</div>';

                        //element.html(html);

                        element.attr({
                            'uib-tooltip': event.startText + '-' + event.endText + '\r\n' + event.title,
                            'tooltip-append-to-body': true
                        });
                        
                        $compile(element)($scope);
                    },
                    eventClick: function (event, jsEvent, view) {
                        if (vm.selectedEvent != event) {
                            if (vm.selectedEventElement) $(vm.selectedEventElement).removeClass('mcs-calendar-event-selected');
                            vm.selectedEvent = event;
                            vm.selectedEventElement = this;
                            $(this).addClass('mcs-calendar-event-selected');
                        }
                    },
                    viewRender: function (view, element) {
                        vm.selectedEvent = null;
                        vm.selectedEventElement = null;
                    }
                }
            };

            vm.events = [];

            $scope.initEvent = function (event) {
                event.className = "mcs-calendar-event";
                if (event.duration < 0) event.color = '#a0a0a0';
                else if (event.duration < 1) event.color = '#d6487e';
                else event.color = '#82af6f';
            };

            vm.schedules = function (start, end, timezone, callback) {
                blockUI.start();
                //简单查询
                courseService.searchCourseScheduleList(vm.criteria, 'simple').then(function (data) {
                    if (data.Code == 0) {
                        var events = data.Data.List;
                        angular.forEach(events, function (event, index, array) {
                            $scope.initEvent(event);
                        });
                        callback(events);
                    } else {
                        bootbox.alert({ title: "错误", message: data.Message });
                    }
                    blockUI.stop();
                });
            };
            vm.eventSources = [vm.events, vm.schedules];

            

            //简单查询按钮的事件
            $scope.simpleSearch = function () {

            };

            //排课按钮的事件
            $scope.addSchedule = function () {
                $uibModal.open({
                    templateUrl: '/app/course/course-schedule-add.html',
                    controller: 'CourseMainAddScheduleController',
                    size: 'md',
                    resolve: {
                        item: function () {
                            return 'mytest';
                        }
                    },
                    backdrop: 'static'
                }).result.then(function (schedule) {
                    vm.events.push({
                        id: 'test',
                        title: '高三生物 张庆民',
                        start: '2016-03-18 20:00',
                        end: '2016-03-18 21:00',
                        stick: true
                    });
                });
            };

            $scope.queryTeacherList = function (query) {
                return courseService.queryTeacherList(query);
            };
        }
    ]);
})();