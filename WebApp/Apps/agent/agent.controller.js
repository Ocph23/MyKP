angular.module("agent.controllers", [])
    .controller('AgentHomeController', AgentHomeController)
    .controller('AgentCreateManifestController', AgentCreateManifestController)
    ;
function AgentHomeController($scope, UserServices, AgentHomeService) {

    $scope.Agent = {};

    UserServices.getPetugas().then(
        function (response) {
            $scope.User = response;
            AgentHomeService.get($scope.User.AgentId).then(function (res) {
                $scope.Agent = res;
            });
        }
            
          
    );
}



function AgentCreateManifestController($scope) {
    $scope.Saved = false;
}