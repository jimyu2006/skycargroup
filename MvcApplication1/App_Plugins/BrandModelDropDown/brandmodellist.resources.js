angular.module('umbraco.resources').factory('brandmodelListResource', function ($q, $http) {
     	return {
         		getAll: function () {
         		    return $http.get("/umbraco/Surface/Brand/GetBrands");
         		},
         		getBrands: function () {
         		    return $http.get("/umbraco/Surface/Brand/GetBrands");
         		},
         		getModels: function () {
         		    return $http.get("/umbraco/Surface/Brand/GetModels");
         		}
     	};
7 });