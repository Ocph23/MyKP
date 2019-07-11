angular.module("agent.services", [])
    .factory('AgentHomeService', AgentHomeService)
    .factory('ManifestServices', ManifestServices)
    .factory('AgentInvoiceServices', AgentInvoiceServices)
    .factory('AgentDashboardServices', AgentDashboardServices)
    ;

function AgentHomeService($http, $state, AuthServices, $q, MessageServices) {
    var service = {};
    var datas = [];

    return {
        get: get, update: update, setAgent:setAgent, getAgent: getAgent, setUserProfile: setUserProfile, getUserProfile: getUserProfile
    };



    function setAgent(agent) {
        sessionStorage.setItem("Agent", JSON.stringify(agent));
    }
    function getAgent() {

        var agent = sessionStorage.getItem("Agent");
        return JSON.parse(sessionStorage.Agent);

    }

    function setUserProfile(data) {
        service.UserProfile = data;
    }

    function getUserProfile() {
        return service.UserProfile;
    }

    function get(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/agent/' + agentId
        }).then(function (response) {
            service.Agent = response.data;
            deffer.resolve(response.data);

        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }
    

    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'agent/UpdateProfile?agentId=' + model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

}

function ManifestServices($http, AuthServices, $q, MessageServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteManifest, update: update, getByManifestById: getByManifestById, deleteItem:deleteItem,
        addNewItem: addNewItem, updateItem: updateItem, find: findSTT, findbyManifestNumber: findbyManifestNumber
    };



    function getByManifestById(id) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/Manifest/' + id
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }


    function get(agentId) {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Manifest?agentId=' + agentId
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
            });
        } else {
            deffer.resolve(datas);
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/manifest',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }




    function deleteManifest(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/manifest/' + model.Id
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }



    function addNewItem(id,model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/manifest/additem?id='+id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function updateItem(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Put',
            url: 'api/manifest/updateitem?id=' + id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/manifest/deleteitem?id=' + model.Id
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }



    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/manifest/'+model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function findSTT(id,model) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/manifest/find?id='+id+'&stt='+model.STT
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }



    function findbyManifestNumber(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/manifest/findManifestByAgentId?agentid=' +id + '&manifestNumber=' + model.STT
        }).then(function (response) {
            deffer.resolve(response.data);
            }, function (error) {
                deffer.reject(error.data.Message);
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }



}


function AgentInvoiceServices($http, AuthServices, $q, MessageServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };


    function get(agentId) {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Invoice/getbyagentid?id=' + agentId
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
            });
        } else {
            deffer.resolve(datas);
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/Invoice/addpembayaran',
            data: model
        }).then(function (response) {
            model.Payments.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function addNewItem(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/manifest/additem?id=' + id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function updateItem(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Put',
            url: 'api/manifest/updateitem?id=' + id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/manifest/' + model.Id
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }



    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/manifest/' + model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


}




function AgentDashboardServices($http, $state, AuthServices, $q, MessageServices) {
    return {
        invoiceCount: invoiceCount, manifestCount: manifestCount,
        sttNotHaveStatus: sttNotHaveStatus, invoiceDeadline: invoiceDeadline
    };


    function invoiceCount(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/AgentDashboar/InvoiceCount/' + agentId
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

    function manifestCount(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/AgentDashboar/ManifestCount/' + agentId
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

    function sttNotHaveStatus(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/AgentDashboar/STTNotHaveStatus/' + agentId
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

    function invoiceDeadline(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/AgentDashboar/InvoiceDeadLine/' + agentId
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

}
