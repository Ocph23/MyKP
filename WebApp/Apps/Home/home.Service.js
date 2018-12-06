
angular.module("home.services", [])
    .factory('AuthServices', AuthServices)
    .factory('MessageServices', MessageServices)
    .factory('CityServices', CityServices)
    .factory('PriceServices', PriceServices)
    ;

function MessageServices($rootScope) {
    var alertService = {};
    $rootScope.alerts = [];

    // will automatidally close
    // types are success, warning, info, danger
    alertService.info = function ( msg) {

        alertMX(msg,"info");

        window.setTimeout(function () {
            $("#boxMX").click();
        }, 3000);
    };


    alertService.error = function (msg) {

        alertMX(msg,"error");

        window.setTimeout(function () {
            $("#boxMX").click();
        }, 2500);
    };

    function alertMX(t, type) {
        $("body").append($("<div id='boxMX'><p class='msgMX'></p></div>"));
        $('.msgMX').text(t);
        //var popMargTop = ($('#boxMX').height() + 24) / 2, popMargLeft = ($('#boxMX').width() + 24) / 2;

        if (type === "error")
            $('#boxMX').css({ 'background': '#dc3545' }).fadeIn(600);
        if (type === "info")
            $('#boxMX').css({ 'background': ' #17a2b8' }).fadeIn(600);
      
        $("#boxMX").click(function () { $(this).remove(); });
    };



    return alertService;

}



function AuthServices($http, $state, $rootScope, MessageServices, $q, UserServices) {
    //var def = $q.defer();

    var service = {
        login: login, register: register, getAgentProfile: getAgentProfile
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
            sessionStorage.setItem("AuthToken", result.access_token);
            sessionStorage.setItem("TokenType", result.token_type);
            sessionStorage.setItem("UserName", user.Email);
            sessionStorage.setItem("Role", result.roles);
            NProgress.done();
            $state.go(result.roles.toLowerCase());

            }, function errorCallback(response) {
                if (response.data.Message !== undefined)
                    MessageServices.add("warning", response.data.Message);
                else
                    MessageServices.error("You Not Have Access");
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


    function getAgentProfile() {
        var def = $q.defer();
        $http({
            method: 'Get',
            url: '/agent/getuserprofile',
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

function CityServices($http, $state, $rootScope, MessageServices, $q, UserServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get() {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/City'
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
            url: 'api/City',
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


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/City/' + model.Id
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
            url: 'api/City',
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

function PriceServices($http, $state, $rootScope, MessageServices, $q, UserServices) {
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
                url: 'api/Price/' + agentId
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
            url: 'api/Price',
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


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/Price/' + model.Id
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
            url: 'api/Price',
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
;