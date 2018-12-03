angular.module("admin.controllers", [])
    .controller('AdminUserController', AdminUserController)
    .controller('DashboardController', DashboardController)
    .controller('AdminAgentController', AdminAgentController)
  
    ;


function AdminUserController($scope, AuthServices) {
    //var data = AuthServices.getAgentProfile();
}


function DashboardController() {

}


function AdminAgentController($scope, AdminService,MessageServices) {
    AdminService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });     
}