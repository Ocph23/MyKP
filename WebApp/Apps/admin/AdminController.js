angular.module("admin.controllers", [])
    .controller('AdminUserController', AdminUserController)
    .controller('DashboardController', DashboardController)
    .controller('AdminAgentController', AdminAgentController)
    .controller('AdminUserManageController', AdminUserManageController)
    .controller('AdminCityManageController', AdminCityManageController)
    .controller('AdminPriceManageController', AdminPriceManageController)
  
    ;


function AdminUserController($scope, AuthServices) {
    //var data = AuthServices.getAgentProfile();
}


function DashboardController() {

}




function AdminAgentController($scope, AdminService, MessageServices, AdminManageUserService) {
    $scope.Selected = {};
    $scope.model = {};
    AdminService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
        });

    $scope.SaveAgent = function (data) {
        data.AgentId = 0;
        if (data.AgentId == undefined) {
            AdminService.post(data).then(function (response) {
                $scope.Datas.push(response);
            });
        } else {
            AdminService.update(data).then(function (response) {
                MessageServices.add("info", "Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        AdminService.delete($scope.SelectedItem).then(function (response) {
            MessageServices.add("info", "Success");
        });
    };




}


function AdminUserManageController($scope, $stateParams, $state, AdminManageUserService) {
    var data = $stateParams.data;
    if (data === null)
        $state.go('admin.agent');
    else
        $scope.Agent = data;

    $scope.Selected = {};
    $scope.model = {};
    AdminManageUserService.get($scope.Agent.AgentId).then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });

    $scope.SaveAgent = function (data) {
       
        if (data.AgentId === undefined) {
            
            AdminManageUserService.post($scope.Agent.AgentId,data).then(function (response) {
                $scope.Datas.push(response);
            });
        } else {
            AdminManageUserService.update(data).then(function (response) {
                MessageServices.add("info", "Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        AdminManageUserService.delete($scope.Agent.AgentId, $scope.SelectedItem ).then(function (response) {
            MessageServices.add("info", "Success");
        });
    };



}


function AdminCityManageController($scope,  MessageServices, CityServices) {
    $scope.Selected = {};
    $scope.model = {};
    CityServices.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.error(error);
    });

    $scope.SaveAgent = function (data) {
        data.AgentId = 0;
        if (data.Id === undefined) {
            CityServices.post(data).then(function (response) {
                MessageServices.info("Success");
            });
        } else {
            CityServices.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        CityServices.delete($scope.SelectedItem).then(function (response) {
            MessageServices.info("Success");
        });
    };


}


function AdminPriceManageController($scope, $stateParams, $state, MessageServices, CityServices, PriceServices) {
    var data = $stateParams.data;
    if (data === null)
        $state.go('admin/agent');
    else
        $scope.Agent = data;

    $scope.Selected = {};
    $scope.CitySelected;
    $scope.PortType;
    $scope.model = {};
    $scope.Cities = [];
    $scope.Ports = ["Sea", "Air"];
    PriceServices.get($scope.Agent.AgentId).then(function (response) {
        $scope.Datas = response;
        CityServices.get().then(function (response) {
            $scope.Cities = response;
        }, function (error) {
            MessageServices.error(error);
        });
    }, function (error) {
        MessageServices.error(error);
    });

    $scope.SaveAgent = function (data) {
        if (data.Id === undefined) {
            data.AgentId = $scope.Agent.AgentId;
            data.CityId = $scope.CitySelected.Id;
            data.PortType = $scope.PortType;

            PriceServices.post(data).then(function (response) {
                MessageServices.info("Success");
            });
        } else {
            PriceServices.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        PriceServices.delete($scope.SelectedItem).then(function (response) {
            MessageServices.info("Success");
        });
    };
}