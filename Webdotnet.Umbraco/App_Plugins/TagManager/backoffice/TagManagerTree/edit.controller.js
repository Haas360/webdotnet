angular.module("umbraco").controller("TagManager.TagManagerEditController",
	function ($scope, $routeParams, $location, TagManagerResource, notificationsService, navigationService, $route) {

	    TagManagerResource.getById($routeParams.id).then(function (response) {
	        $scope.cmsTags = response.data;
	        $scope.selectedTag = $routeParams.id;
	    });

	    $scope.save = function (cmsTags) {
	        TagManagerResource.save(cmsTags).then(function (response) {
	            $scope.cmsTags = response.data;
	            var pathArray = ['-1', 'tagGroup-' + cmsTags.group];
	            notificationsService.success("Success", "'" + cmsTags.tag + "' has been saved");
	            pathArray.push(cmsTags.id);
	            navigationService.syncTree({ tree: 'TagManagerTree', path: pathArray, forceReload: true, activate: false }).then(
                    function (syncArgs) {
                        if ($routeParams.method == "edit") {
                            $location.path(syncArgs.node.routePath);
                            $route.reload();
                        }
                    });
	        });
	    };

	    $scope.deleteTag = function (cmsTags) {
	        TagManagerResource.deleteTag(cmsTags).then(function (response) {
	            $scope.cmsTags = response.data;
	            notificationsService.success("Success", "'" + cmsTags.tag + "' has been deleted.");
	            var pathArray = ['-1'];
	            navigationService.syncTree({ tree: 'TagManagerTree', path: pathArray, forceReload: true });
	            treeService.removeNode($scope.currentNode);
	        });
	    };
	});