angular.module("agent.controllers", [])
    .controller('AgentHomeController', AgentHomeController)
    .controller('AgentManifestController', AgentManifestController)
    .controller('AgentCreateManifestController', AgentCreateManifestController)
    .controller('AgentFindSTTController', AgentFindSTTController)
    .controller('AgentInvoiceController', AgentInvoiceController)
    .controller('AgentPriceController', AgentPriceController)
    .controller('AgentDashboardController', AgentDashboardController)
    ;

function AgentHomeController($scope, AuthServices, AgentHomeService, $state) {
    AuthServices.userRoleIs("Agent");
    $scope.Agent = {};

    AuthServices.getUserAgentProfile().then(
        function (response) {
            $scope.User = response;
            AgentHomeService.setUserProfile(response);
            AgentHomeService.get($scope.User.AgentId).then(function (res) {
                $scope.Agent = res;
                AgentHomeService.setAgent(res);
            });
        }
    );

    $scope.logout = function () {
        AuthServices.logout();
    };

    $state.go('agentdashboard');

}

function AgentManifestController($scope, AuthServices, ManifestServices, AgentHomeService) {
    AuthServices.userRoleIs("Agent");

    $scope.Datas = [];
    $scope.Init = function () {
        $scope.Agent = AgentHomeService.getAgent();
        ManifestServices.get($scope.Agent.AgentId).then(function (response) {
            $scope.Datas = response;
            $scope.Saved = true;
        });
    };
}

function AgentCreateManifestController($scope, AuthServices, ManifestServices, AgentHomeService,CityServices,$stateParams) {
    AuthServices.userRoleIs("Agent");
    $scope.Saved = false;
    $scope.WeightTypes = ["Weight", "WeightVolume", "Volume"];
    $scope.Ports = ["Air", "Sea"];
    $scope.ShippingMode = ["Land","Air", "Sea"];
    $scope.Agent = AgentHomeService.getAgent();
    
    $scope.model = $stateParams.data;
    if ($scope.model === null) {
        $scope.model = {};
        $scope.model.SendedDate = new Date();
        $scope.model.CreateDate = new Date();
        $scope.model.User = AgentHomeService.getUserProfile();
    } else {
        $scope.Saved = true;
        $scope.model.Items = [];
        ManifestServices.getByManifestById($scope.model.Id).then(function (data) {
            data.Items.forEach(element => {
                $scope.model.Items.push(element);
            });
        });
    }

    $scope.model.UserName = sessionStorage.getItem("LoginName");
   

    $scope.Cities = [];
    CityServices.get().then(function (response) { $scope.Cities = response;});

    $scope.CreateManifest = function (manifest) {

        if (manifest.Id === undefined) {
            manifest.AgentId = $scope.Agent.AgentId;
            manifest.AgentAdminId = AgentHomeService.getUserProfile().Id;
            ManifestServices.post(manifest).then(function (response) {
                manifest.Id = response.Id;
                $scope.Saved = true;
            });
        } else {
            ManifestServices.update(manifest).then(function (response) {

            });

            $scope.data = {};
        }
      
    };


    $scope.AddNewItem = function (item) {
        if (item.Id === undefined) {
            item.ManifestId = $scope.model.Id;
            item.CityId = item.City.Id;
            ManifestServices.addNewItem(item.ManifestId, item).then(function (response) {
                $scope.model.Items.push(response.data);
            });
            item = {};
        } else {
            item.CityId = item.City.Id;
            ManifestServices.updateItem(item.ManifestId, item).then(function (data) {
                
            });
        }
    };


    $scope.SelectedItem = function (item) {
        $scope.Selected = item;
    };


    $scope.EditItem = function (item) {
        $scope.data = item;
    };

    $scope.delete = function () {
        if ($scope.Selected !== undefined || $scope.Selected !== null) {
            ManifestServices.delete($scope.Selected).then(function (response) {
                var index = $scope.model.Items.indexOf($scope.Selected);
                $scope.model.Item.slice(index, 1);
                $scope.Selected = {};
            });
        }
    };


    $scope.print = function () {
        window.print();
        $state.go('invoices');
    }


}

function AgentFindSTTController($scope, AgentHomeService, ManifestServices) {
    $scope.Agent = AgentHomeService.getAgent();
    $scope.Items = [];
    $scope.Find = function (item) {
        $scope.Items.splice(0, $scope.Items.length);
        ManifestServices.find($scope.Agent.AgentId, item).then(function (response) {
            $scope.Items = response.data;
        });
    };


    $scope.Detail = function (data) {
        $scope.data = data;
    };
}

function AgentInvoiceController($scope, AgentInvoiceServices, AgentHomeService) {
    $scope.Statuses = ["Baru", "Tunda", "Panjar", "Pelunasan", "Lunas", "Batal"];
    $scope.Agent = AgentHomeService.getAgent();
    AgentInvoiceServices.get($scope.Agent.AgentId).then(function (response) {
        $scope.Sources = response;
    });

    $scope.SelectedItem = function (item) {
        $scope.model = {};
        $scope.model.Invoice = item;
        $scope.model.InvoiceId = item.Id;
        $scope.model.NumberView = item.NumberView;
       
        $scope.model.Id = 0;
        var paid = 0;
        item.Payments.forEach((x) => {
            paid += x.AmountPaid;
        });

        $scope.model.Paid = paid;
        $scope.model.AmountPaid = item.GrandTotal - paid;
        $scope.ChangeStatus($scope.model);
    };


    $scope.ChangeStatus = function (model) {
        model.CanSave = true;
        if (model.AmountPaid <= 0 || model.Invoice.GrandTotal < model.AmountPaid + model.Paid) {
            model.CanSave = false;
        }

        if (model.AmountPaid + model.Paid === model.Invoice.GrandTotal)
            model.Status = "Pelunasan";
        else if(model.AmountPaid + model.Paid < model.Invoice.GrandTotal) {
            model.Status = "Panjar";
        }
    };

    $scope.SavePayment = function (model) {
        AgentInvoiceServices.post(model);
    };

}

function AgentPriceController($scope, PriceServices, AgentHomeService, MessageServices, AuthServices) {
    $scope.Agent = AgentHomeService.getAgent();
    $scope.Ports = ["Sea", "Air", "Land"];
    PriceServices.get($scope.Agent.AgentId).then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.error(error);
        });


    $scope.print = function () {
        AuthServices.print();
    };

}

function AgentDashboardController($scope, AgentDashboardServices, AgentHomeService) {
    $scope.labels = ["Ada Status", "Tidak Ada Status"];
    $scope.colors = ['#07BEB8', '#FE4A49', '#803690', '#00ADF9', '#FDB45C', '#949FB1', '#4D5360'];
   

    NProgress.start();
    setTimeout(Init,1000);

    function Init() {

        $scope.Agent = AgentHomeService.getAgent();
        var id = $scope.Agent.AgentId;
        AgentDashboardServices.invoiceCount(id).then(function (response) {
            $scope.InvoiceCount = response;
            AgentDashboardServices.manifestCount(id).then(function (response) {
                $scope.ManifestCount = response;
                AgentDashboardServices.sttNotHaveStatus(id).then(function (response) {
                    $scope.SttNotHaveStatus = response;
                    $scope.data = [$scope.SttNotHaveStatus.Count - $scope.SttNotHaveStatus.Datas.length, $scope.SttNotHaveStatus.Datas.length];
                    AgentDashboardServices.invoiceDeadline(id).then(function (response) {
                        $scope.InvoiceDeadline = response;
                    
                        NProgress.done();
                    });
                });
            });
        });

    }
}