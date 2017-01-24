angular.module("umbraco").controller("brandModelController", function ($scope, $q, brandmodelListResource, notificationsService) {
    brandmodelListResource.getAll().then(function (response) {
        $scope.parentListItems = response.data.Brands;
        $scope.allModels = response.data.Models;
        $scope.childListItems = angular.copy($scope.allModels);

        if ($scope.model.value) {
            populateChildListItems($scope.model.value.BrandName);
        }
    }, function (response) {
        notificationsService.error("Error", "Error loading brands");
        console.log(response.data);
    });

    $scope.update = function () {
        var selectedBrandCode = $scope.model.value.BrandName;
        populateChildListItems(selectedBrandCode);
    }

    function populateChildListItems(selectedBrandCode) {
        $scope.childListItems = $scope.allModels.filter(function (item, index, array) {
            if (item.BrandName == selectedBrandCode) {
                return item;
            }
        });
    }
}
);