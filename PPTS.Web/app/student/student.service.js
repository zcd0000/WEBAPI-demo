'use strict';

angular.module('ppts.student.services').factory('studentService', ['$http', function ($http) {
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

	//获取学员列表初始化数据
	service.getStudentMainIndex = function () {
		return $http.get('http://localhost:18721/api/Student/Index')
                    .then(getDataComplete)
                    .catch(getDataFailed);
	};

	//简单查询学员数据
	service.searchStudentList = function (criteria, mode) {
		var url = 'http://localhost:18721/api/Student/' + (mode == 'advance' ? 'Advance' : 'Simple') + 'Search';
		return $http.post(url, criteria)
                    .then(getDataComplete)
                    .catch(getDataFailed);
	};

	//查询已办列表
	service.searchDoneList = function (searchCondition) {
		return $http.post('/api/Todo/Search', { searchCondition: createConditionParam(searchCondition) })
                    .then(getDataComplete)
                    .catch(getDataFailed);
	};

	return service;
}]);