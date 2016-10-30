mugMatcher.controller("find", ["$scope", "$http", function ($scope, $http) {
	$scope.selectedPath = "";
	$scope.personFound = null;

	$scope.setPath = function (path) {
		$scope.selectedPath = path;
	}

	$scope.findPerson = function (path) {
		$scope.personFound = null;
		$http.post("/find/findPerson", { file: path }).then(function (result) {
			$scope.personFound = result.data;
			console.log($scope.personFound);
		});
	}
}]);