
angular.module("admin.services", [])
    .factory('UserServices', UserServices)
    .factory('AdminService', AdminService)
    .factory('AdminManageUserService', AdminManageUserService)

;




function UserServices($http, $state,$rootScope,$q,MessageServices) {

    this.token = sessionStorage.getItem("AuthToken");
    function logout(){
        $state.go('login');
    }

    function getToken() {
        return sessionStorage.getItem("AuthToken");
    }

    function getHeaders() {
        if (getToken() === null)
            $state.go('login');
        else

            return {'Content-Type': 'application/json', 'Authorization': 'Bearer ' + getToken() };
    }

    function getUser() {
        return sessionStorage.getItem("UserName");
    }

    function getPetugas() {

        var defer = $q.defer();
        $http({
            headers: getHeaders(),
            method: 'Get',
            url: 'api/agent/getuserprofile'
        }).then(function (response) {
            defer.resolve( response.data);
            }, function (error) {
                MessageServices.error(error.data.Message);
                defer.reject();

            });
        return defer.promise;
    }


    return {
        logout: logout, getToken: getToken, getHeaders: getHeaders, getUser: getUser, getPetugas: getPetugas
    };
}

function AdminService($http, $state, $rootScope,$q) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post:post,delete:deleteItem,update:update
    };

    function get() {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Agent'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
            }, function (error) {
                deffer.reject();
                alert(error.data.Message);
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
            url: 'api/Agent',
            data:model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/Agent/'+model.AgentId
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }



    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/Agent',
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




function AdminManageUserService($http, $state, $rootScope, $q) {
    var service = {};
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'agent/AgentUsers?agentId=' + agentId
        }).then(function (response) {
            instance = true;
            response.data.forEach(item => {
                datas.push(item);
            });
            deffer.resolve(datas);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }

    function post(agentId,model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'agent/AddNewUser?agentId=' + agentId,
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(id,model) {
        var deffer = $q.defer();
        $http({
            method: 'Put',
            url: 'agent/ChangeActive?agentId=' + id,
            data:model
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            alert(error.data.Message);
        });

        return deffer.promise;
    }




    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'agent/UpdateProfile?agentId='+model.Id,
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