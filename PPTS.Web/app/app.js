'use strict';

angular.module('ppts.constants', []);
angular.module('ppts.services', []);
angular.module('ppts.controllers', []);
angular.module('ppts.directives', []);

angular.module('ppts.common.directives', []);
angular.module('ppts.common.controllers', []);
angular.module('ppts.common.services', []);

angular.module('ppts.index.services', []);
angular.module('ppts.index.controllers', []);

angular.module('ppts.student.services', []);
angular.module('ppts.student.controllers', []);

angular.module('ppts.potential-customer.services', []);
angular.module('ppts.potential-customer.controllers', []);

angular.module('ppts.course.services', []);
angular.module('ppts.course.controllers', []);

angular.module('ppts', ['ngCookies', 'ngSanitize', 'ngTagsInput',
    'ui.router', 'ui.bootstrap', 'blockUI', 'ui.calendar',
    'ppts.constants', 'ppts.services', 'ppts.controllers', 'ppts.directives',
    'ppts.common.controllers', 'ppts.common.directives', 'ppts.common.services',
    'ppts.index.controllers', 'ppts.index.services',
    'ppts.student.controllers', 'ppts.student.services',
    'ppts.potential-customer.controllers', 'ppts.potential-customer.services',
    'ppts.course.controllers', 'ppts.course.services'
])
.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/index");

    $stateProvider.state('index', {
        url: '/index',
        templateUrl: '/app/index/main.html'
    }).state('student', {
        url: '/student',
        templateUrl: '/app/student/main.html',
        controller: 'StudentMainController'
    }).state('potential-customer-list', {
        url: '/potential-customer/list',
        templateUrl: '/app/potential-customer/list/main.html',
        controller: 'PotentialCustomerMainController'
    }).state('course-schedule', {
        url: '/course-schedule',
        templateUrl: '/app/course/main.html',
        controller: 'CourseScheduleMainController'
    });
}])
.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded';
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

    // Override $http service's default transformRequest
    $httpProvider.defaults.transformRequest = [function (data) {
        /**
		 * The workhorse; converts an object to x-www-form-urlencoded serialization.
		 * @param {Object} obj
		 * @return {String}
		 */
        var param = function (obj) {
            var query = '';
            var name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value instanceof Object) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value !== undefined && value !== null) {
                    query += encodeURIComponent(name) + '='
							+ encodeURIComponent(value) + '&';
                }
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        return angular.isObject(data) && String(data) !== '[object File]'
				? param(data)
				: data;
    }];
}])
.config(['blockUIConfig', function (blockUIConfig) {
    blockUIConfig.message = '正在加载数据 ...';
    blockUIConfig.autoBlock = false;
}])
.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});