mugMatcher
    .controller("find", ["$scope", "$http", function ($scope, $http) {
	$scope.selectedPath = "";
	$scope.personFound = null;
	$scope.hasFileBeenPicked = false;
    $scope.file = null;

	$scope.setPath = function (path) {
		$scope.selectedPath = path;
	}

	$scope.findPerson = function () {
		//$scope.personFound = null;
		//$http.post("/find/findPerson", { file: path }).then(function (result) {
		//	$scope.personFound = result.data;
		//	console.log($scope.personFound);
	    //});
	    var data = new FormData();
	    for (var x = 0; x < $scope.files.length; x++) {
	        data.append("file" + x, $scope.files[x]);
	    }
	    $http({
	        type: "POST",
	        url: '/find/findPerson',
	        contentType: false,
	        processData: false,
	        data: data,
	        success: function (result) {
	            console.log(result);
	        },
	        error: function (xhr, status, p3, p4) {
	            var err = "Error " + " " + status + " " + p3 + " " + p4;
	            if (xhr.responseText && xhr.responseText[0] == "{")
	                err = JSON.parse(xhr.responseText).Message;
	            console.log(err);
	        }
	    });
	}

	$scope.filePicked = function (element) {
	    $scope.files = element.files;
	    $scope.hasFileBeenPicked = true;
	    $scope.$apply();
	}

    
}]);