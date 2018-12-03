
angular.module("admin.services", [])
.factory('UserServices', UserServices)
 

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
            url: '/Account/PetugasProfile'
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


    ;