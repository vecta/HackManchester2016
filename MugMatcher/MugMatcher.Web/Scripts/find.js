mugMatcher
    .controller("find", ["$scope", "$http", function ($scope, $http) {
	$scope.selectedPath = "";
	$scope.personFound = null;
	$scope.hasFileBeenPicked = false;
	$scope.files = null;
    var name = '',
        age = '',
        gender = '',
        lat = '',
        long = '';

	$scope.setPath = function (path) {
		$scope.selectedPath = path;
	}

	$scope.findPerson = function () {

	    var data = new FormData();
	    for (var x = 0; x < $scope.files.length; x++) {
	        data.append("file" + x, $scope.files[x]);
	    }
	    data.append("details",
	    {
	        name : name,
	        age : age,
	        gender : gender,
	        lat : lat,
	        long : long
	    });

	    $.ajax({
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

	    var reader = new FileReader();

	    reader.onload = function (e) {
	        $('#missing-persons-img')
                .attr('src', e.target.result)
                .width(300);
	    };

	    reader.readAsDataURL($scope.files[0]);
	}
    
}]);