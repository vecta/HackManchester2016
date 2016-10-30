mugMatcher.directive("scopeInit", function () {
	return {
		restrict: 'E',
		link: function (scope, element, attributes) {
			var content = element[0].innerHTML.trim();
			scope[attributes.value] = JSON.parse(content);
			element.remove();
		}
	}
});

