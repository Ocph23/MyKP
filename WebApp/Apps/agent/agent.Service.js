angular.module("agent.services", [])
    .factory('AgentHomeService', AgentHomeService)
    //.factory('InboxServices', InboxServices)
    ;



function AgentHomeService($http, $state, $rootScope, $q, MessageServices) {
    var service = {};
    var datas = [];

    return {
        get: get, update: update
    };

    function get(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/agent/' + agentId
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
            url: 'agent/UpdateProfile?agentId=' + model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }

}

