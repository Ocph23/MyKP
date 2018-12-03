
angular.module("home.services", [])
    .factory('AuthServices', AuthServices)
    .factory('MessageServices', MessageServices)
    ;

function MessageServices($rootScope) {
    var alertService = {};
    $rootScope.alerts = [];

    // will automatidally close
    // types are success, warning, info, danger
    alertService.add = function (type, msg, delay) {
        var alert = { 'type': type, 'msg': msg };
        $rootScope.alerts.push(alert);
        if (!delay) {
            delay = 2500; // default delay is 2500ms
        }
        window.setTimeout(function () {
            var index = $rootScope.alerts.indexOf(alert);
            if (index > -1) {
                $rootScope.alerts.splice(index, 1);
                $rootScope.$apply(); // refresh GUI
            }
        }, delay);
    }

    return alertService;

}



function AuthServices($http, $state, $rootScope, MessageServices, $q, UserServices) {
    //var def = $q.defer();

    var service = {
        login: login, register: register, getProfile: getProfile
    };


    function login(user) {
        var data = "grant_type=password&username=" + user.Email + "&password=" + user.Password;
        NProgress.start();
        $http({
            method: 'POST',
            url: '/Token',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: data
        }).then(function successCallback(response) {
            var result = response.data;
            sessionStorage.setItem("AuthToken", result.token);
            sessionStorage.setItem("TokenType", "bearer");
            sessionStorage.setItem("UserName", user.Email);
            NProgress.done();
            $state.go(result.roles.toLowerCase());

            }, function errorCallback(response) {
                MessageServices.add("warning", response.data.Message);
            NProgress.done();
        });
    }


    function register(model) {
        var def = $q.defer();
        $http({
            method: 'Post',
            url: '/account/register',
            data: model
        }).then(function successCallback(response) {
            MessageServices.success("Register Success, Silahkan Login");
            def.resolve(response);
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            def.reject();
            });
        return def.promise;
    }


    function getProfile() {
        var def = $q.defer();
        $http({
            method: 'Get',
            url: '/account/PetugasProfile',
            headers: UserServices.getHeaders()
        }).then(function successCallback(response) {
            def.resolve(response.data);
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            def.reject();
            });
        return def.promise;
    }



    return service;
}


;