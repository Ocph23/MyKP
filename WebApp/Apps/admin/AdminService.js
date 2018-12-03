
angular.module("admin.services", [])
    .factory('UserServices', UserServices)
    .factory('AdminService', AdminService)

;




function UserServices($http, $state,$rootScope) {

    this.token = sessionStorage.getItem("AuthToken");
    function logout(){
        $state.go('login');
    }

    function getToken() {
        return sessionStorage.getItem("AuthToken");
    }

    function getHeaders() {
        if (getToken() == null)
            $state.go('login');
        else

        return { 'Authorization': 'Bearer ' + getToken() }
    }

    function getUser() {
        return sessionStorage.getItem("UserName");
    }

    function getPetugas() {
        $rootScope.DataUser = {};
        $http({
            method: 'Get',
            url: '/Agent/PetugasProfile'
        }).then(function (response) {
            $rootScope.DataUser = response.data;

            }, function (error) {
                alert(error.message);

            });
        return $rootScope.DataUser;
    }


    return {
        logout: logout, getToken: getToken, getHeaders: getHeaders, getUser: getUser, getPetugas: getPetugas
    }
}

function AdminService($http, $state, $rootScope,$q) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get
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
                alert(error.message);
            });
        } else {
            deffer.resolve(datas);
        }

        return deffer.promise;
    }
}