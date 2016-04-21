'use strict';
/* App Module */
var app = angular.module('app', []);
app.directive('mcsTree', function () {
    return {
        require: '?ngModel',
        restrict: 'A',
        link: function ($scope, element, attrs, ngModel) {
            //$(element).addClass('ztree');

            var settingConfig = attrs.mcsTree ? $scope.$eval(attrs.mcsTree) : {};

            var setting = {
                check: {
                    enable: false,
                    chkboxType: {
                        "Y": "ps",
                        "N": "ps"

                    }
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                async: {
                    enable: false,
                    autoParam: ["id"],
                    type: 'post',
                    dataType: "json"
                },
                callback: {}
            };



            angular.extend(setting, settingConfig);
            if (setting.async && setting.async.enable && !setting.async.dataFilter) {
                setting.async.dataFilter = function (treeId, parentNode, responseData) {
                    return responseData.Data.List;
                };
            }
            setting.callback.onClick = function (event, treeId, treeNode, clickFlag) {
                $scope.$apply(function () {
                    ngModel.$setViewValue(treeNode);
                });
            };

            var nodes = setting.nodes ? setting.nodes : [];
            if (!setting.nodes && setting.async && setting.async.url) {
                $.post(setting.async.url, {
                    id: null
                }, function (data) {
                    nodes = data.Data.List;
                    $.fn.zTree.init(element, setting, nodes);
                });
            } else {
                $.fn.zTree.init(element, setting, nodes);
            }
        }
    };
});