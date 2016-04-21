'use strict';

angular.module('ppts.course.services').factory('courseService', ['$http', '$q', function ($http, $q) {
	var service = {};

	function createConditionParam(searchCondition) {
		var param = { Page: 1, Limit: 10, ConditionItems: [] };
		var propertyName;
		for (propertyName in searchCondition) {
			if (propertyName == "Page" || propertyName == "Limit") param[propertyName] = searchCondition[propertyName];
			else param.ConditionItems.push({ ConditionName: propertyName, ConditionValue: searchCondition[propertyName], Operator: '=' });
		}
		return param;
	}

	function getDataComplete(response) {
		return response.data;
	}

	function getDataFailed(error) {
		var errorHTML = '';
		errorHTML += '<table style="width:1000px;" class="table table-bordered"><tr><td class="mcs-form-td-left">Message：</td><td><input onclick="$(this).select()" type="text" class="form-control" value="' + error.data.Message + '" readonly /></td></tr>';
		errorHTML += '<tr><td class="mcs-form-td-left">ExceptionMessage：</td><td><input onclick="$(this).select()" type="text" class="form-control" value="' + error.data.ExceptionMessage + '" readonly /></td></tr>';
		errorHTML += '<tr><td class="mcs-form-td-left">ExceptionType：</td><td><input onclick="$(this).select()" type="text" class="form-control" value="' + error.data.ExceptionType + '" readonly /></td></tr>';
		errorHTML += '<tr><td class="mcs-form-td-left">StackTrace：</td><td><textarea onclick="$(this).select()" rows="10" class="form-control" readonly>' + error.data.StackTrace.replace('\r\n', '<br/>') + '</textarea></td></tr></table>';
		return { Code: -1, Message: errorHTML };
	}

	//获取排课计划初始化数据
	service.getCourseScheduleMainIndex = function () {
		return $http.get('http://localhost:2676/api/Schedule/Index')
                    .then(getDataComplete)
                    .catch(getDataFailed);
	};

	//简单查询日程数据
	service.searchCourseScheduleList = function (criteria, mode) {
		var url = 'http://localhost:2676/api/Schedule/' + (mode == 'advance' ? 'Advance' : 'Simple') + 'Search';
		return $http.post(url, criteria)
                    .then(getDataComplete)
                    .catch(getDataFailed);
	};

    //模糊查询老师
	service.queryTeacherList = function (keyword) {
	    var url = 'http://localhost:2676/api/Schedule/QueryTeacher';
	    return $http.post(url, {Keyword: keyword} ).then(function (response) {
	        var teachers = response.data.Data.List;
	        return teachers;
	    });
	};

	return service;
}]);